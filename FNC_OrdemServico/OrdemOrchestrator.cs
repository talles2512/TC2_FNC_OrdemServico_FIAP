using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using OrdemServico.Core.Domain;

namespace FNC_OrdemServico
{
    public static class OrdemOrquestrator
    {
        [FunctionName("OrdemOrquestrator")]
        public static async Task<string> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var ordem = context.GetInput<Ordem>();

            var tempoExpiracaoGarantia = await context.CallActivityAsync<dynamic>("VerificaTipoProdutoFunction", ordem);
            var estaNaGarantia = await context.CallActivityAsync<dynamic>("VerificaGarantiaProdutoFunction", (tempoExpiracaoGarantia, ordem));
            var retorno = await context.CallActivityAsync<dynamic>("VerificaDefeitoProdutoFunction", (estaNaGarantia, ordem));
            var confirmacao = await context.CallActivityAsync<dynamic>("EmitirOrdemGarantiaFunction", (retorno, ordem));
            return confirmacao;
        }
    }
}