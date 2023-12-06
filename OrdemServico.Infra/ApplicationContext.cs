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
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            
        }

        public DbSet<Ordem> Ordens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrdemConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
