using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using OrdemServico.Core.Domain;
using System;
using System.Threading.Tasks;

namespace FNC_OrdemServico.Activities
{
    public static class VerificaGarantiaProdutoFunction
    {
        [FunctionName("VerificaGarantiaProdutoFunction")]
        public static async Task<bool> Run(
            [ActivityTrigger] (int, Ordem) input,
            ILogger log)
        {
            var (tempoGarantiaMeses, ordem) = input;

            var estaNaGarantia = false;

            if(!(ordem.DataAquisicao == DateTime.MinValue))
            {
                var dataExpiracaoGarantia = ordem.DataAquisicao.AddMonths(tempoGarantiaMeses);

                estaNaGarantia = dataExpiracaoGarantia > DateTime.Now;
            }

            return await Task.FromResult(estaNaGarantia);
        }
    }
}