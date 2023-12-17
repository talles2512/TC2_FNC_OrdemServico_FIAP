using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
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
        public static async Task<string> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            try
            {
                var ordem = context.GetInput<Ordem>();

                var retornoInserirOrdem = await context.CallActivityAsync<ResultadoOperacao<Ordem>>("OrdemBancoFunction", (OperacaoBanco.Inserir, ordem));

                if (retornoInserirOrdem.Sucesso)
                {
                    var processamentoOrdem = await ProcessarOrdem(context, ordem);

                    return string.Empty;
                }
                else
                {
                    //Em caso de falha no insert da Ordem
                    //var confirmacao = await context.CallActivityAsync<dynamic>("EmitirOrdemManutencao", ordem);

                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static async Task<ProcessamentoOrdem> ProcessarOrdem(IDurableOrchestrationContext context, Ordem ordem)
        {
            var processamentoOrdem = NovoProcessamentoOrdem(ordem);

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
                var resultadoOperacao = await context.CallActivityAsync<ResultadoOperacao<int>>("VerificaTipoProdutoFunction", ordem);

                if (resultadoOperacao.Sucesso)
                {
                    resultadoOperacao = await context.CallActivityAsync<ResultadoOperacao<int>>("GeraPrazoManutencaoFunction", ordem);

                    if (resultadoOperacao.Sucesso)
                    {
                        processamentoOrdem.StatusOrdem = StatusOrdem.APROVADA;
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

            if (retornoAlterarProcessamentoOrdem.Sucesso)
            {
                //var confirmacao = await context.CallActivityAsync<dynamic>("EmitirOrdemManutencao", ordem);

                //return confirmacao;
            }

            return processamentoOrdem;
        }

        private static ProcessamentoOrdem NovoProcessamentoOrdem(Ordem ordem)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                OrdemId = ordem.Id,
                StatusOrdem = StatusOrdem.ANALISE,
                DataCriacao = DateTime.Now,
                DataAtualizacao = DateTime.Now,
                Ativo = true
            };
        }
    }
}