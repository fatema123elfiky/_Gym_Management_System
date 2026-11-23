using GymManagementSystem.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.BLL.Services.Members.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string? Photo { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Gender { get; set; }
        // and if we want address we could make it string , we restructure our data according to the view
    }
}
