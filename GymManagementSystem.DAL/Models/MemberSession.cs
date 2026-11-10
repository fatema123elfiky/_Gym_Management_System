using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DAL.Models
{
    // that class should have pk , so it should inherit from base entity
    // that is right and another opinion , to take composite key and that will let us make configuration
    public class MemberSession : BaseEntity
    {
        public Member Member { get; set; } = default!;
        public int MemberId{ get; set; }

        public Session Session { get; set; } = default!;
        public int SessionId { get; set; }

        public int BookingDays { get; set; }
        public bool IsAttended { get; set; } 

    }
}
