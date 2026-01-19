using CleanTeeth.Application.Contracts.Repositories.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanTeathPersistance.UnitsOfWork
{
    public class UnitOfWorkEFCore : IUnitOfWork
    {
        private readonly CleanTeethDbContext context;

        public UnitOfWorkEFCore(CleanTeethDbContext context)
        {
            this.context = context;
        }
        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public Task RollbackAsync()
        {
            return Task.CompletedTask;
        }
    }
}
