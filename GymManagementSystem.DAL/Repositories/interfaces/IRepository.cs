using GymManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DAL.Repositories.interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false);
        public Task<TEntity?> GetByIdAsync(int id);
        public Task<int> AddAsync(TEntity plan);
        public Task<int> UpdateAsync(TEntity plan);
        public Task<int> DeleteAsync(TEntity plan);
    }
}
