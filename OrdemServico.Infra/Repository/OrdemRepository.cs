using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdemServico.Infra.Repository
{
    public class OrdemRepository
    {
        private readonly ApplicationContext DbContext;

        public OrdemRepository(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
