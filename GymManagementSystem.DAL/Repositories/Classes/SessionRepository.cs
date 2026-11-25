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
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(GymDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategory()
        {
            return await _dbContext.Sessions.AsNoTracking().Include(x => x.Category).Include(x => x.Trainer).ToListAsync(); // returned i querable so make it ienum
        }

        public async Task<int> GetCountOfBookedSlotsAsync(int sessionId)
        {
            return await _dbContext.MemberSession.AsNoTracking().Where(x => x.SessionId == sessionId).CountAsync();
        }
    }
}
