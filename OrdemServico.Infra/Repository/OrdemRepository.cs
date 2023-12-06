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

        public async Task<Ordem> ObterOrdem(string nserie)
        {
            return await _dbSet.FirstOrDefaultAsync(o => o.NumeroSerie == nserie);
        }

        public async Task InserirOrdem(Ordem ordem)
        {
            await _dbSet.AddAsync(ordem);
            _dbContext.SaveChanges();
        }
    }
}