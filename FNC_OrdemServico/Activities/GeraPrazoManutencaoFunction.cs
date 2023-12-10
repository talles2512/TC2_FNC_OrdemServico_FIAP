using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using OrdemServico.Core.Domain;
using OrdemServico.Core.Objects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FNC_OrdemServico.Activities
{
    public static class GeraPrazoManutencaoFunction
    {
        [FunctionName("GeraPrazoManutencaoFunction")]
        public static async Task<ResultadoOperacao<int>> Run(
            [ActivityTrigger] Ordem ordem,
            ILogger log)
        {
            var resultado = CalculaPrazoManutencao(ordem);

            return await Task.FromResult(resultado);
        }

        private static ResultadoOperacao<int> CalculaPrazoManutencao(Ordem ordem)
        {
            var sucesso = false;
            var mensagem = string.Empty;
            var prazoManutencaoDias = 0;

            if (Defeito.Defeitos.Any(x => x.Contains(ordem.TipoDefeito)))
            {
                var rand = new Random();
                prazoManutencaoDias = rand.Next(10, 30);
                sucesso = true;
            }
            else
                mensagem = "Defeito fora da cobertura de serviço";

            return new()
            {
                Sucesso = sucesso,
                Mensagem = mensagem,
                Retorno = prazoManutencaoDias
            };
        }
    }
}