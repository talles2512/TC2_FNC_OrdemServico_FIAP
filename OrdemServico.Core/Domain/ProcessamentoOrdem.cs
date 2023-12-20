using OrdemServico.Core.Enums;

namespace OrdemServico.Core.Domain
{
    public class ProcessamentoOrdem
    {
        public Guid Id { get; set; }
        public Guid OrdemId { get; set; }
        public bool EstaNaGarantia { get; set; }
        public int PrazoConclusaoDiasUteis { get; set; }
        public StatusOrdem StatusOrdem { get; set; }
        public string MotivoRecusa { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}