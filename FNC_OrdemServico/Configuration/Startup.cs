using FNC_OrdemServico.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrdemServico.Infra;
using OrdemServico.Infra.Repository;

[assembly: FunctionsStartup(typeof(Startup))]

namespace FNC_OrdemServico.Configuration
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;
            var connectionString = configuration.GetConnectionString("ApplicationConnectionString");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<OrdemRepository>();
            builder.Services.AddScoped<ProcessamentoOrdemRepository>();
        }
    }
}
