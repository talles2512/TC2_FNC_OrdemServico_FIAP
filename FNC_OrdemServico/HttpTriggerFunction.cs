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
using System.Collections.Generic;
using System.Linq;

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

            var (ordemValida, mensagensErros) = ValidarOrdem(ordem);

            if (ordemValida)
            {
                string instanceId = await starter.StartNewAsync("OrdemOrquestrator", ordem);
                return starter.CreateCheckStatusResponse(req, instanceId);
            }
            else
            {
                return new BadRequestObjectResult(new { Erros = mensagensErros });
            }
        }

        private static Tuple<bool, List<string>> ValidarOrdem(Ordem ordem)
        {
            ordem.Id = new Guid();
            var mensagensErros = new List<string>();

            if (string.IsNullOrEmpty(ordem.NomeCliente) || ordem.NomeCliente.Length < 5)
            {
                mensagensErros.Add("Por favor preencha o nome corretamente.");
            }
            else if (string.IsNullOrEmpty(ordem.Endereco) || ordem.Endereco.Length < 5)
            {
                mensagensErros.Add("Por favor preencha o endereço corretamente.");
            }
            else if (string.IsNullOrEmpty(ordem.NumeroTelefone) || ordem.NumeroTelefone.Length < 10)
            {
                mensagensErros.Add("Por favor preencha o nº de telefone corretamente.");
            }
            else if (string.IsNullOrEmpty(ordem.Email) || ordem.Email.Length < 10)
            {
                mensagensErros.Add("Por favor preencha o e-mail corretamente.");
            }
            else if (string.IsNullOrEmpty(ordem.TipoProduto) || ordem.TipoProduto.Length < 5)
            {
                mensagensErros.Add("Por favor preencha o tipo do produto corretamente.");
            }
            else if (string.IsNullOrEmpty(ordem.MarcaProduto) || ordem.MarcaProduto.Length < 5)
            {
                mensagensErros.Add("Por favor preencha a marca do produto corretamente.");
            }
            else if (string.IsNullOrEmpty(ordem.ModeloEquipamento) || ordem.ModeloEquipamento.Length < 5)
            {
                mensagensErros.Add("Por favor preencha o modelo do equipamento corretamente.");
            }
            else if (string.IsNullOrEmpty(ordem.NumeroSerie) || ordem.NumeroSerie.Length < 5)
            {
                mensagensErros.Add("Por favor preencha o nº de série corretamente.");
            }
            else if (string.IsNullOrEmpty(ordem.TipoDefeito) || ordem.TipoDefeito.Length < 5)
            {
                mensagensErros.Add("Por favor preencha o tipo do defeito corretamente.");
            }
            else if (string.IsNullOrEmpty(ordem.DescricaoProblema) || ordem.DescricaoProblema.Length < 5)
            {
                mensagensErros.Add("Por favor preencha a descrição do problema corretamente.");
            }
            
            return Tuple.Create(mensagensErros.Count() == 0, mensagensErros);
        }
    }
}
