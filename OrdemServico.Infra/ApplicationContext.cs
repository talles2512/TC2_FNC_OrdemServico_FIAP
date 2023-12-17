using Microsoft.EntityFrameworkCore;
using OrdemServico.Core.Domain;
using OrdemServico.Infra.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdemServico.Infra
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<Ordem> Ordens { get; set; }
        public DbSet<ProcessamentoOrdem> ProcessamentoOrdens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new OrdemConfiguration());
            modelBuilder.ApplyConfiguration(new ProcessamentoOrdemConfiguration());
        }
    }
}
