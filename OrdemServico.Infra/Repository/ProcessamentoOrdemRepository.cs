using Microsoft.EntityFrameworkCore;
using OrdemServico.Core.Domain;

namespace OrdemServico.Infra.Repository
{
    public class ProcessamentoOrdemRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly DbSet<ProcessamentoOrdem> _dbSet;

        public ProcessamentoOrdemRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ProcessamentoOrdem>();
        }

        public async Task<ProcessamentoOrdem> ObterProcessamentoOrdem(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task InserirProcessamentoOrdem(ProcessamentoOrdem processamentoOrdem)
        {
            await _dbSet.AddAsync(processamentoOrdem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AlterarProcessamentoOrdem(ProcessamentoOrdem processamentoOrdem)
        {
            _dbSet.Update(processamentoOrdem);
            await _dbContext.SaveChangesAsync();
        }
    }
}
