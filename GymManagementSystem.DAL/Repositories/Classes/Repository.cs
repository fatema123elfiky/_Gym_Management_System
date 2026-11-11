using GymManagementSystem.DAL.Data.Contexts;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DAL.Repositories.Classes
{
    // same contraint!!
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity , new()
    {
        protected readonly GymDbContext _dbContext;
        private readonly DbSet<TEntity> _set;

        public Repository(GymDbContext dbContext)
        {
            // wrong one the dbcontext should be injected as dependency injection and not creating instance of dbcontext here
            //_dbContext = new GymDbContext();

            _dbContext = dbContext;
            _set = _dbContext.Set<TEntity>();

        }
        public async Task<int> AddAsync(TEntity entity)
        {

            await _set.AddAsync(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(TEntity entity)
        {
            _set.Remove(entity);
            return _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false)
        {
            return tracking ? await _set.ToListAsync() : await _set.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _set.FindAsync(id);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
           _set.Update(entity);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
