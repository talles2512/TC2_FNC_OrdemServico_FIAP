using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using OrdemServico.Core.Domain;
using OrdemServico.Core.Enums;
using System;
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

            var (codigoMarca, mensagemErroMarca) = VerificaMarcaProduto(ordem.MarcaProduto);
            if (codigoMarca == 0)
            {
                return new { Sucesso = false, Mensagem = mensagemErroMarca, TempoGarantia = 0 };
            }

            var (codigoProduto, mensagemErroProduto) = VerificaTipoProduto(ordem.TipoProduto);
            if (codigoProduto == 0)
            {
                return new { Sucesso = false, Mensagem = mensagemErroProduto, TempoGarantia = 0 };
            }

            //Modelos de produtos aqui
            //Verificacao aqui

            var tempoGarantia = CalculaTempoGarantia(codigoMarca, codigoProduto, "");

            return new { Sucesso = true, Mensagem = string.Empty, TempoGarantia = tempoGarantia };
        }

        private static Tuple<int, string> VerificaMarcaProduto(string marca)
        {
            if (Enum.TryParse(typeof(MarcaProduto), marca, out object result))
            {
                MarcaProduto marcaProduto = (MarcaProduto)result;
                int codigo = 0;
                if (marcaProduto is MarcaProduto.Sega)
                {
                    codigo = 1;
                }
                else if (marcaProduto is MarcaProduto.Playstation)
                {
                    codigo = 2;
                }
                else if (marcaProduto is MarcaProduto.Nintendo)
                {
                    codigo = 3;
                }
                else if (marcaProduto is MarcaProduto.Xbox)
                {
                    codigo = 4;
                }

                return Tuple.Create(codigo, string.Empty);
            }
            else
            {
                return Tuple.Create(0, "Marca fora da cobertura de serviço");
            }
        }

        private static Tuple<int, string> VerificaTipoProduto(string produto)
        {
            if (Enum.TryParse(typeof(TipoProduto), produto, out object result))
            {
                TipoProduto tipoProduto = (TipoProduto)result;
                int codigo = 0;
                if (tipoProduto is TipoProduto.Acessorio)
                {
                    codigo = 1;
                }
                else if (tipoProduto is TipoProduto.Componente)
                {
                    codigo = 2;
                }
                else if (tipoProduto is TipoProduto.Console)
                {
                    codigo = 3;
                }
                else if (tipoProduto is TipoProduto.Controle)
                {
                    codigo = 4;
                }

                return Tuple.Create(codigo, string.Empty);
            }
            else
            {
                return Tuple.Create(0, "Produto fora da cobertura de serviço");
            }
        }

        private static int CalculaTempoGarantia(int codigoMarca, int codigoProduto, string modelo)
        {
            int qtdeMeses = 0;

            if (codigoMarca == 1) { qtdeMeses += 3; }
            else if (codigoMarca == 2) { qtdeMeses += 3; }
            else if (codigoMarca == 3) { qtdeMeses += 3; }
            else if (codigoMarca == 4) { qtdeMeses += 3; }

            if (codigoProduto == 1) { qtdeMeses += 9; }
            else if (codigoProduto == 2) { qtdeMeses += 0; }
            else if (codigoProduto == 3) { qtdeMeses += 3; }
            else if (codigoProduto == 4) { qtdeMeses += 0; }

            return qtdeMeses;
        }
    }
}