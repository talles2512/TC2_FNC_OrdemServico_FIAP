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
            builder.Property(x => x.NomeCliente).HasMaxLength(255);
        }
    }
}
