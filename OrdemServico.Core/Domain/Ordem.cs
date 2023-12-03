namespace OrdemServico.Core.Domain
{
    public class Ordem
    {
        public Guid Id { get; set; }
        public string NomeCliente { get; set; }
        public string Endereco { get; set; }
        public string NumeroTelefone { get; set; }
        public string Email { get; set; }
        public string TipoEquipamento { get; set; }
        public string MarcaEquipamento { get; set; }
        public string ModeloEquipamento { get; set; }
        public string NumeroSerie { get; set; }
        public string TipoProblema { get; set; }
        public string DescricaoProblema { get; set; }
        public DateTime ExpiracaoGarantia { get; set; }
        public DateTime DataCriacao { get; set; }
        public int PrazoConclusao { get; set; }
        public DateTime DataConclusao { get; set; }
        public string StatusOrdem { get; set; }
    }
}
