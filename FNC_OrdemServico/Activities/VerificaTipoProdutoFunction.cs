using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using OrdemServico.Core.Domain;
using OrdemServico.Core.Enums;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FNC_OrdemServico.Activities
{
    public static class VerificaTipoProdutoFunction
    {
        [FunctionName("VerificaTipoProdutoFunction")]
        public static async Task<dynamic> Run(
            [ActivityTrigger] Ordem ordem,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request => VerificaTipoProdutoFunction");

            var resultado = CalcularTempoGarantia(ordem);

            return await Task.FromResult(resultado);
        }

        private static dynamic CalcularTempoGarantia(Ordem ordem)
        {
            var sucesso = false;
            var tempoGarantiaMeses = 0;
            var mensagem = string.Empty;

            if (Enum.TryParse(typeof(MarcaProduto), ordem.MarcaProduto, out object resultadoMarca))
            {
                MarcaProduto marcaProduto = (MarcaProduto)resultadoMarca;

                switch (marcaProduto)
                {
                    case MarcaProduto.Sega:
                    case MarcaProduto.Playstation:
                    case MarcaProduto.Nintendo:
                    case MarcaProduto.Xbox:
                        tempoGarantiaMeses += 3;
                        break;
                }

                if (Enum.TryParse(typeof(TipoProduto), ordem.TipoProduto, out object result))
                {
                    TipoProduto tipoProduto = (TipoProduto)result;

                    switch (tipoProduto)
                    {
                        case TipoProduto.Acessorio:
                            tempoGarantiaMeses += 9;
                            break;
                        case TipoProduto.Componente:
                            tempoGarantiaMeses += 0;
                            break;
                        case TipoProduto.Console:
                            tempoGarantiaMeses += 3;
                            break;
                        case TipoProduto.Controle:
                            tempoGarantiaMeses += 0;
                            break;
                    }

                    sucesso = VerificaCoberturaModelo(marcaProduto, tipoProduto, ordem.ModeloEquipamento);

                    if (!sucesso)
                    {
                        tempoGarantiaMeses = 0;
                        mensagem = "Modelo de Produto fora da cobertura de serviço";
                    }
                }
                else
                {
                    tempoGarantiaMeses = 0;
                    mensagem = "Produto fora da cobertura de serviço";
                }
            }
            else
                mensagem = "Marca fora da cobertura de serviço";

            return new {
                Sucesso = sucesso,
                Mensagem = mensagem,
                TempoGarantia = tempoGarantiaMeses
            };
        }


        private static bool VerificaCoberturaModelo(MarcaProduto marcaProduto, TipoProduto tipoProduto, string modelo)
        {
            var cobertura = false;

            switch (marcaProduto)
            {
                case MarcaProduto.Sega:

                    if(tipoProduto is  TipoProduto.Acessorio)
                        cobertura = ModelosSega.ModelosAcessorio.Any(x => x.Contains(modelo));
                    else if(tipoProduto is TipoProduto.Componente)
                        cobertura = ModelosSega.ModelosComponente.Any(x => x.Contains(modelo));
                    else if(tipoProduto is TipoProduto.Console)
                        cobertura = ModelosSega.ModelosConsole.Any(x => x.Contains(modelo));
                    else
                        cobertura = ModelosSega.ModelosControle.Any(x => x.Contains(modelo));
                    break;

                case MarcaProduto.Playstation:

                    if (tipoProduto is TipoProduto.Acessorio)
                        cobertura = ModelosPlaystation.ModelosAcessorio.Any(x => x.Contains(modelo));
                    else if (tipoProduto is TipoProduto.Componente)
                        cobertura = ModelosPlaystation.ModelosComponente.Any(x => x.Contains(modelo));
                    else if (tipoProduto is TipoProduto.Console)
                        cobertura = ModelosPlaystation.ModelosConsole.Any(x => x.Contains(modelo));
                    else
                        cobertura = ModelosPlaystation.ModelosControle.Any(x => x.Contains(modelo));
                    break;

                case MarcaProduto.Nintendo:

                    if (tipoProduto is TipoProduto.Acessorio)
                        cobertura = ModelosNintendo.ModelosAcessorio.Any(x => x.Contains(modelo));
                    else if (tipoProduto is TipoProduto.Componente)
                        cobertura = ModelosNintendo.ModelosComponente.Any(x => x.Contains(modelo));
                    else if (tipoProduto is TipoProduto.Console)
                        cobertura = ModelosNintendo.ModelosConsole.Any(x => x.Contains(modelo));
                    else
                        cobertura = ModelosNintendo.ModelosControle.Any(x => x.Contains(modelo));
                    break;

                case MarcaProduto.Xbox:

                    if (tipoProduto is TipoProduto.Acessorio)
                        cobertura = ModelosXbox.ModelosAcessorio.Any(x => x.Contains(modelo));
                    else if (tipoProduto is TipoProduto.Componente)
                        cobertura = ModelosXbox.ModelosComponente.Any(x => x.Contains(modelo));
                    else if (tipoProduto is TipoProduto.Console)
                        cobertura = ModelosXbox.ModelosConsole.Any(x => x.Contains(modelo));
                    else
                        cobertura = ModelosXbox.ModelosControle.Any(x => x.Contains(modelo));
                    break;
            }

            return cobertura;
        }
    }
}