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
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {



        public PlanRepository(GymDbContext dbContext) : base(dbContext)
        {
            // wrong one the dbcontext should be injected as dependency injection and not creating instance of dbcontext here
            //_dbContext = new GymDbContext();

            // before
            // _dbContext = dbContext;
            //after
            //nothing !! , it is initailized by parent

        }
        // only 
        public async Task<IEnumerable<Plan>> GetAllWithMemberShipAsync(bool tracking = false)
        {
            return tracking ? await _dbContext.Plans.Include(x => x.MemberShips).ToListAsync() : await _dbContext.Plans.Include(x => x.MemberShips).AsNoTracking().ToListAsync();

        }
    }
}

