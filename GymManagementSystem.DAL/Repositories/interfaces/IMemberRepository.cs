using GymManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DAL.Repositories.interfaces
{
    internal interface IMemberRepository : IRepository<Member>
    {
        public IQueryable<Member> GetMemberWithRelatedData(int id);
    }
}
