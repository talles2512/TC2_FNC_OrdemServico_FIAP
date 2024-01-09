using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdemServico.Core.Domain;

namespace OrdemServico.Infra.Configurations
{
    public class OrdemConfiguration : IEntityTypeConfiguration<Ordem>
    {
        public void Configure(EntityTypeBuilder<Ordem> builder)
        {
            builder.ToTable("Ordem");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.NomeCliente).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Endereco).HasMaxLength(255).IsRequired();
            builder.Property(x => x.NumeroTelefone).HasMaxLength(13).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(255).IsRequired();
            builder.Property(x => x.TipoProduto).HasMaxLength(50).IsRequired();
            builder.Property(x => x.MarcaProduto).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ModeloEquipamento).HasMaxLength(100).IsRequired();
            builder.Property(x => x.NumeroSerie).HasMaxLength(100).IsRequired();
            builder.Property(x => x.TipoDefeito).HasMaxLength(50).IsRequired();
            builder.Property(x => x.DescricaoProblema).HasMaxLength(100).IsRequired();
            builder.Property(x => x.DataAquisicao);
            builder.Property(x => x.DataCriacao).HasDefaultValue(DateTime.Now);
            builder.HasOne(x => x.ProcessamentoOrdem);
        }
    }
}
