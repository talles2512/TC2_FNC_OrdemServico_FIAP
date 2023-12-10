using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrdemServico.Core.Domain;
using OrdemServico.Core.Objects;

namespace FNC_OrdemServico
{
    public static class OrdemOrquestrator
    {
        [FunctionName("OrdemOrquestrator")]
        public static async Task<string> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var ordem = context.GetInput<Ordem>();

            var retornoVerificacaoTipoProduto = await context.CallActivityAsync<ResultadoOperacao<int>>("VerificaTipoProdutoFunction", ordem);

            if (retornoVerificacaoTipoProduto.Sucesso)
            {
                var estaNaGarantia = await context.CallActivityAsync<bool>("VerificaGarantiaProdutoFunction", (retornoVerificacaoTipoProduto.Retorno, ordem));
                var retornoPrazoManutencao = await context.CallActivityAsync<ResultadoOperacao<int>>("GeraPrazoManutencaoFunction", ordem);

                var sucesso = retornoPrazoManutencao.Sucesso;
            }


            var confirmacao = await context.CallActivityAsync<dynamic>("EmitirOrdemManutencao", ordem);

            return confirmacao;
        }
    }
}