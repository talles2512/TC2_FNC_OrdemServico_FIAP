using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using OrdemServico.Core.Domain;

namespace FNC_OrdemServico
{
    public static class HttpTriggerFunction
    {
        [FunctionName("HttpTriggerFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [DurableClient] IDurableOrchestrationClient starter, 
            ILogger log)
        {
            log.LogInformation("Nova requisição recebida!");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            log.LogInformation($"Body: {requestBody}");

            var ordem = JsonConvert.DeserializeObject<Ordem>(requestBody);

            if (!OrdemValida(ordem))
                return null;

            string instanceId = await starter.StartNewAsync("OrdemOrquestrator", ordem);

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

        private static bool OrdemValida(Ordem ordem)
        {
            return true;
        }
    }
}
