using Microsoft.EntityFrameworkCore;
using OrdemServico.Core.Domain;

namespace OrdemServico.Infra.Repository
{
    public class OrdemRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly DbSet<Ordem> _dbSet;

        public OrdemRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Ordem>();
        }

        public async Task<Ordem> ObterOrdemProcessamentoOrdem(string nserie)
        {
            var ordem = await _dbSet
                .Include(x => x.ProcessamentoOrdem)
                .FirstOrDefaultAsync(o => o.NumeroSerie == nserie && o.ProcessamentoOrdem.Ativo);

            return ordem;
        }

        public async Task InserirOrdem(Ordem ordem)
        {
            await _dbSet.AddAsync(ordem);
            await _dbContext.SaveChangesAsync();
        }
    }
}