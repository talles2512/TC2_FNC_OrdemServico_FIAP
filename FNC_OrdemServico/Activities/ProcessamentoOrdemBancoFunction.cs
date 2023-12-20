using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using OrdemServico.Core.Domain;
using OrdemServico.Core.Enums;
using OrdemServico.Core.Objects;
using OrdemServico.Infra.Repository;
using System;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;

namespace FNC_OrdemServico.Activities
{
    public class ProcessamentoOrdemBancoFunction
    {
        private readonly ProcessamentoOrdemRepository _repository;

        public ProcessamentoOrdemBancoFunction(ProcessamentoOrdemRepository repository)
        {
            _repository = repository;
        }

        [FunctionName("ProcessamentoOrdemBancoFunction")]
        public async Task<ResultadoOperacao<ProcessamentoOrdem>> Run(
            [ActivityTrigger] (OperacaoBanco, ProcessamentoOrdem) input,
            ILogger log)
        {
            var (operacao, processamentoOrdem) = input;

            switch (operacao)
            {
                //case OperacaoBanco.Obter:
                //    return await ObterProcessamentoOrdem(ordem.NumeroSerie);

                case OperacaoBanco.Inserir:
                    return await InserirProcessamentoOrdem(processamentoOrdem);

                case OperacaoBanco.Alterar:
                    return await AlterarProcessamentoOrdem(processamentoOrdem);

                default:
                    return await InserirProcessamentoOrdem(processamentoOrdem);
            }
        }

        public async Task<ResultadoOperacao<ProcessamentoOrdem>> InserirProcessamentoOrdem(ProcessamentoOrdem processamentoOrdem)
        {
            var sucesso = true;
            var mensagem = string.Empty;

            try
            {
                await _repository.InserirProcessamentoOrdem(processamentoOrdem);
            }
            catch (Exception ex)
            {
                sucesso = false;
                mensagem = ex.Message;
            }

            return new()
            {
                Sucesso = sucesso,
                Mensagem = mensagem,
                Retorno = processamentoOrdem
            };
        }

        public async Task<ResultadoOperacao<ProcessamentoOrdem>> AlterarProcessamentoOrdem(ProcessamentoOrdem processamentoOrdem)
        {
            var sucesso = true;
            var mensagem = string.Empty;
            var processamentoOrdemDb = await _repository.ObterProcessamentoOrdem(processamentoOrdem.Id);

            if (processamentoOrdemDb is null)
            {
                sucesso = false;
                mensagem = "Ordem não encontrada";
            }
            else
            {
                processamentoOrdemDb.StatusOrdem = processamentoOrdem.StatusOrdem;
                processamentoOrdemDb.DataAtualizacao = processamentoOrdem.DataAtualizacao;
                processamentoOrdemDb.Ativo = processamentoOrdem.Ativo;
                processamentoOrdemDb.PrazoConclusaoDiasUteis = processamentoOrdem.PrazoConclusaoDiasUteis;
                processamentoOrdemDb.MotivoRecusa = processamentoOrdem.MotivoRecusa;
                processamentoOrdemDb.EstaNaGarantia = processamentoOrdem.EstaNaGarantia;

                await _repository.AlterarProcessamentoOrdem(processamentoOrdemDb);
            }

            return new()
            {
                Sucesso = sucesso,
                Mensagem = mensagem,
                Retorno = processamentoOrdemDb
            };
        }
    }
}