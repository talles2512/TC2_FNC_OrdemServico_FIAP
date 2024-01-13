using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrdemServico.Core.Domain;
using OrdemServico.Core.Enums;
using OrdemServico.Core.Objects;

namespace FNC_OrdemServico
{
    public static class OrdemOrquestrator
    {
        [FunctionName("OrdemOrquestrator")]
        public static async Task<dynamic> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            try
            {
                var ordem = context.GetInput<Ordem>();
                var retornoInserirOrdem = await context.CallActivityAsync<ResultadoOperacao<Ordem>>("OrdemBancoFunction", (OperacaoBanco.Inserir, ordem));

                if (retornoInserirOrdem.Sucesso)
                {
                    var processamentoOrdem = await ProcessarOrdem(context, ordem);

                    return new EmissaoOrdem
                    (
                        ordem.Id,
                        ordem.NomeCliente,
                        ordem.Endereco,
                        ordem.NumeroTelefone,
                        ordem.Email,
                        ordem.TipoProduto,
                        ordem.MarcaProduto,
                        ordem.ModeloEquipamento,
                        ordem.NumeroSerie,
                        processamentoOrdem.StatusOrdem,
                        processamentoOrdem.MotivoRecusa,
                        processamentoOrdem.PrazoConclusaoDiasUteis,
                        processamentoOrdem.EstaNaGarantia
                    );
                }
                else
                {
                    return retornoInserirOrdem;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static async Task<ProcessamentoOrdem> ProcessarOrdem(IDurableOrchestrationContext context, Ordem ordem)
        {
            var processamentoOrdem = NovoProcessamentoOrdem(context, ordem);
            
            var retornoObterProcessamentoOrdem = await context.CallActivityAsync<ResultadoOperacao<Ordem>>("OrdemBancoFunction", (OperacaoBanco.Obter, ordem));

            if (retornoObterProcessamentoOrdem.Sucesso)
            {
                processamentoOrdem.StatusOrdem = StatusOrdem.RECUSADA;
                processamentoOrdem.MotivoRecusa = "Já existe uma ordem igual a essa em andamento";

                await context.CallActivityAsync<ResultadoOperacao<ProcessamentoOrdem>>("ProcessamentoOrdemBancoFunction", (OperacaoBanco.Inserir, processamentoOrdem));
                return processamentoOrdem;
            }

            var retornoInserirProcessamentoOrdem = await context.CallActivityAsync<ResultadoOperacao<ProcessamentoOrdem>>("ProcessamentoOrdemBancoFunction", (OperacaoBanco.Inserir, processamentoOrdem));

            if (!retornoInserirProcessamentoOrdem.Sucesso)
            {
                processamentoOrdem.StatusOrdem = StatusOrdem.RECUSADA;
                processamentoOrdem.MotivoRecusa = retornoInserirProcessamentoOrdem.Mensagem;
                processamentoOrdem.Ativo = false;
            }
            else
            {
                var resultadoOperacao = await context.CallActivityAsync<ResultadoOperacao<int>>("VerificaCoberturaServicoFunction", ordem);

                if (resultadoOperacao.Sucesso)
                {
                    resultadoOperacao = await context.CallActivityAsync<ResultadoOperacao<int>>("GeraPrazoManutencaoFunction", ordem);

                    if (resultadoOperacao.Sucesso)
                    {
                        processamentoOrdem.StatusOrdem = StatusOrdem.APROVADA;
                        processamentoOrdem.PrazoConclusaoDiasUteis = resultadoOperacao.Retorno;
                        processamentoOrdem.EstaNaGarantia = await context.CallActivityAsync<bool>("VerificaGarantiaProdutoFunction", (resultadoOperacao.Retorno, ordem));
                    }
                    else
                    {
                        processamentoOrdem.StatusOrdem = StatusOrdem.RECUSADA;
                        processamentoOrdem.MotivoRecusa = resultadoOperacao.Mensagem;
                        processamentoOrdem.Ativo = false;
                    }
                }
                else
                {
                    processamentoOrdem.StatusOrdem = StatusOrdem.RECUSADA;
                    processamentoOrdem.MotivoRecusa = resultadoOperacao.Mensagem;
                    processamentoOrdem.Ativo = false;
                }
            }

            var retornoAlterarProcessamentoOrdem = await context.CallActivityAsync<ResultadoOperacao<ProcessamentoOrdem>>("ProcessamentoOrdemBancoFunction", (OperacaoBanco.Alterar, processamentoOrdem));

            return retornoAlterarProcessamentoOrdem.Retorno;
        }

        private static ProcessamentoOrdem NovoProcessamentoOrdem(IDurableOrchestrationContext context, Ordem ordem)
        {
            return new()
            {
                Id = context.NewGuid(),
                OrdemId = ordem.Id,
                StatusOrdem = StatusOrdem.ANALISE,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Ativo = true
            };
        }
    }
}