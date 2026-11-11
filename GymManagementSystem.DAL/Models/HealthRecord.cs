using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.DAL.Models
{
    public class HealthRecord :BaseEntity
    {
        public string Height { get; set; } = default!;
        public string Weight { get; set; } = default!;
        public string BloodType { get; set; } = default!;
        public string? Note { get; set; } = default!;

        // one to one relationship with member
        public Member Member { get; set; } = default!; // if one contains reference to other , it will make one to one relationship and put the nagvition property very well
        public int MemberId { get; set; } 
        // it is better to make it  as the ef core would make without the one to one relationship as we made the classes ,but it would not be able where to put fk as it could be in any side

    }
}
