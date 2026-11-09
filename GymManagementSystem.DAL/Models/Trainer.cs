using GymManagementSystem.DAL.Models.Enums;

namespace GymManagementSystem.DAL.Models
{
    public class Trainer : GymUser
    {

        // CreatedAt => HireDate
        public Specialty Specialty { get; set; }

    }
}
