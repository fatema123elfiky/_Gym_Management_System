namespace GymManagementSystem.DAL.Models
{
    public class Member : GymUser
    {
        // sometimes audit columns help as needed in bussienss
        // CreatedAt  => JoinDate

        public string? Photo { get; set; }

        // one to one relationship with HealthRecord
        public HealthRecord HealthRecord { get; set; } = default!;

        // one to many relationship with MemberShip
        public ICollection<MemberShip> MemberShips { get; set; } = default!;

        // one to many relationship with MemberSession
        public ICollection<MemberSession> MemberSessions { get; set; } = default!;

    }
}
