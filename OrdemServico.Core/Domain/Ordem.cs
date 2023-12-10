namespace OrdemServico.Core.Domain
{
    public class Ordem
    {
        public Guid Id { get; set; }
        public string NomeCliente { get; set; }
        public string Endereco { get; set; }
        public string NumeroTelefone { get; set; }
        public string Email { get; set; }
        public string TipoProduto { get; set; }
        public string MarcaProduto { get; set; }
        public string ModeloEquipamento { get; set; }
        public string NumeroSerie { get; set; }
        public string TipoDefeito { get; set; }
        public string DescricaoProblema { get; set; }
        public DateTime DataAquisicao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool EstaNaGarantia { get; set; }
        public int PrazoConclusaoDiasUteis { get; set; }
        public string StatusOrdem { get; set; }
    }
}
