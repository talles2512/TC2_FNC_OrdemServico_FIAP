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

            log.LogInformation($"VerificaGarantiaProdutoFunction - {ordem.Id} => Verificar se está na garantia. Data da aquisição '{ordem.DataAquisicao}'");

            var estaNaGarantia = false;

            if(!(ordem.DataAquisicao == DateTime.MinValue))
            {
                var dataExpiracaoGarantia = ordem.DataAquisicao.AddMonths(tempoGarantiaMeses);

                estaNaGarantia = dataExpiracaoGarantia > DateTime.Now;
            }

            log.LogInformation($"VerificaGarantiaProdutoFunction - {ordem.Id} => Garantia '{estaNaGarantia}'");

            return await Task.FromResult(estaNaGarantia);
        }
    }
}