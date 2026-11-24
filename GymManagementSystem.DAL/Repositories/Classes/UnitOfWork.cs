using GymManagementSystem.DAL.Data.Contexts;
using GymManagementSystem.DAL.Models;
using GymManagementSystem.DAL.Repositories.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _dbContext;

        public UnitOfWork(GymDbContext dbContext, IMemberRepository memberRepository)
        {
            _dbContext = dbContext;
            this.memberRepository = memberRepository;
        }

        private Dictionary<string , object> Repos = [];

        public IMemberRepository memberRepository { get ; set; }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var type = typeof(TEntity).Name;

            if(!string.IsNullOrEmpty(type) && Repos.TryGetValue(type,out object value))
            {
                return (IRepository<TEntity>)Repos[type];
            }

            var repo = new Repository<TEntity>(_dbContext);
            Repos[type] = repo;
            return repo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
