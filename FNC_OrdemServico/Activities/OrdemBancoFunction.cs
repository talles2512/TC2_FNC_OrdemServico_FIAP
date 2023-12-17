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
    public class OrdemBancoFunction
    {
        private readonly OrdemRepository _repository;

        public OrdemBancoFunction(OrdemRepository repository)
        {
            _repository = repository;
        }

        [FunctionName("OrdemBancoFunction")]
        public async Task<ResultadoOperacao<Ordem>> Run(
            [ActivityTrigger] (OperacaoBanco, Ordem) input,
            ILogger log)
        {

            var (operacao, ordem) = input;

            switch (operacao)
            {
                case OperacaoBanco.Obter:
                    return await ObterOrdemProcessamentoOrdem(ordem.NumeroSerie);

                case OperacaoBanco.Inserir:
                    return await InserirOrdem(ordem);

                //case OperacaoBanco.Alterar:
                //    return await AlterarProcessamentoOrdem(processamentoOrdem);

                default:
                    return await ObterOrdemProcessamentoOrdem(ordem.NumeroSerie);
            }
        }

        public async Task<ResultadoOperacao<Ordem>> ObterOrdemProcessamentoOrdem(string numeroSerie)
        {
            var sucesso = true;
            var mensagem = string.Empty;
            var ordemDb = await _repository.ObterOrdemProcessamentoOrdem(numeroSerie);

            if (ordemDb is null)
            {
                sucesso = false;
                mensagem = "ProcessamentoOrdem não encontrado";
            }

            return new()
            {
                Sucesso = sucesso,
                Mensagem = mensagem,
                Retorno = ordemDb
            };
        }

        public async Task<ResultadoOperacao<Ordem>> InserirOrdem(Ordem ordem)
        {
            var sucesso = true;
            var mensagem = string.Empty;

            try
            {
                await _repository.InserirOrdem(ordem);
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
                Retorno = ordem
            };
        }
    }
}