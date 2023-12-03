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
    public static class VerificaTipoProdutoFunction
    {
        [FunctionName("VerificaTipoProdutoFunction")]
        public static async Task<string> Run(
            [ActivityTrigger] string name,
            ILogger log)
        {
            //Verificar o tipo do produto, a marca, e o modelo
            log.LogInformation("C# HTTP trigger function processed a request.");

            //Retorna o tempo de expiração da garantia
            return await Task.FromResult("");
        }
    }
}
