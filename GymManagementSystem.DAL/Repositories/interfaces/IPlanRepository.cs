using GymManagementSystem.DAL.Models;

namespace GymManagementSystem.DAL.Repositories.interfaces;

// importance of that interface , what if about mock repository 

public interface IPlanRepository : IRepository<Plan>
{
    public Task<IEnumerable<Plan>> GetAllWithMemberShipAsync(bool tracking = false);


}

