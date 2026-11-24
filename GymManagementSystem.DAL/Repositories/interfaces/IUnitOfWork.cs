using GymManagementSystem.DAL.Models;


namespace GymManagementSystem.DAL.Repositories.interfaces
{
    public interface IUnitOfWork
    {
        public IMemberRepository memberRepository { get; set; }
        // first method return any type of repo i want
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();

        Task<int> SaveChangesAsync();
    }
}
