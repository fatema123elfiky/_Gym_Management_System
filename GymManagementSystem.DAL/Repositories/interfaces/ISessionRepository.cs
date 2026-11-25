using GymManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DAL.Repositories.interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        public Task<IEnumerable<Session>> GetAllSessionsWithTrainerAndCategory();
        public Task<int> GetCountOfBookedSlotsAsync(int sessionId);
    }
}
