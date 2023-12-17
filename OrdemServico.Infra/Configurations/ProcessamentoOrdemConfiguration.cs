using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrdemServico.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace OrdemServico.Infra.Configurations
{
    public class ProcessamentoOrdemConfiguration : IEntityTypeConfiguration<ProcessamentoOrdem>
    {
        public void Configure(EntityTypeBuilder<ProcessamentoOrdem> builder)
        {
            builder.ToTable("ProcessamentoOrdem");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StatusOrdem).HasConversion<string>()
                .HasMaxLength(20).IsRequired();
            builder.Property(x => x.EstaNaGarantia).IsRequired();
            builder.Property(x => x.PrazoConclusaoDiasUteis).IsRequired();
            builder.Property(x => x.DataCriacao).IsRequired();
            builder.Property(x => x.DataAtualizacao).IsRequired();
            builder.Property(x => x.Ativo).IsRequired();
            builder.Property(x => x.MotivoRecusa).HasMaxLength(100);
        }
    }
}
