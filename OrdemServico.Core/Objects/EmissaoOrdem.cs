using Newtonsoft.Json;
using OrdemServico.Core.Enums;
using System.Text.Json.Serialization;

namespace OrdemServico.Core.Objects
{
    public class EmissaoOrdem
    {
        [JsonProperty("protocolo")]
        public string Protocolo { get; set; }

        [JsonProperty("nome-cliente")]
        public string NomeCliente { get; set; }

        [JsonProperty("endereco")]
        public string Endereco { get; set; }

        [JsonProperty("telefone")]
        public string NumeroTelefone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("produto")]
        public string TipoProduto { get; set; }

        [JsonProperty("marca")]
        public string MarcaProduto { get; set; }

        [JsonProperty("modelo")]
        public string ModeloEquipamento { get; set; }

        [JsonProperty("numero-serie")]
        public string NumeroSerie { get; set; }

        [JsonProperty("status")]
        public string StatusOrdem { get; set; }

        [JsonProperty("motivo-recusa")]
        public string MotivoRecusa { get; set; }

        [JsonProperty("prazo-conclusao")]
        public int PrazoConclusaoDiasUteis { get; set; }

        [JsonProperty("tem-garantia")]
        public string Garantia { get; set; }

        public EmissaoOrdem(Guid id,
                    string nomeCliente,
                    string endereco,
                    string numeroTelefone,
                    string email,
                    string tipoProduto,
                    string marcaProduto,
                    string modeloEquipamento,
                    string numeroSerie,
                    StatusOrdem statusOrdem,
                    string motivoRecusa,
                    int prazoConclusaoDiasUteis,
                    bool estaNaGarantia)
        {
            Protocolo = id.ToString().Replace("-","").ToUpper();
            NomeCliente = nomeCliente;
            Endereco = endereco;
            NumeroTelefone = numeroTelefone;
            Email = email;
            TipoProduto = tipoProduto;
            MarcaProduto = marcaProduto;
            ModeloEquipamento = modeloEquipamento;
            NumeroSerie = numeroSerie;
            StatusOrdem = statusOrdem.ToString();
            MotivoRecusa = motivoRecusa;
            PrazoConclusaoDiasUteis = prazoConclusaoDiasUteis;
            Garantia = estaNaGarantia? "Sim" : "Não";
        }
    }
}