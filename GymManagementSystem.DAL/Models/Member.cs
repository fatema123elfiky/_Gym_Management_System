namespace GymManagementSystem.DAL.Models
{
    public class Member : GymUser
    {
        // sometimes audit columns help as needed in bussienss
        // CreatedAt  => JoinDate

        public string? Photo { get; set; }

    }
}
