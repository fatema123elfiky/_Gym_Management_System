using GymManagementSystem.DAL.Models.Enums;

namespace GymManagementSystem.DAL.Models
{
    public class Trainer : GymUser
    {

        // CreatedAt => HireDate
        public Specialty Specialty { get; set; }

        // one to many relationship with session
        public ICollection<Session> Sessions { get; set; } = default!;// to let framework make that object
    }
}
