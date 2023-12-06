using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FNC_OrdemServico.Activities
{
    public static class EmitirOrdemGarantiaFunction
    {
        [FunctionName("EmitirOrdemGarantiaFunction")]
        public static async Task<string> Run(
            [ActivityTrigger] string name,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return await Task.FromResult("OK");
        }
    }
}