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

namespace FNC_OrdemServico
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
