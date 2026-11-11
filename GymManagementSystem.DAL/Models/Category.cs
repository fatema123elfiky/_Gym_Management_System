namespace GymManagementSystem.DAL.Models
{
    public class Category : BaseEntity
    {

        public string CategoryName { get; set; } = default!;

        // one to many relationships with sessions
        public ICollection<Session> Sessions { get; set; } = default!;

    }
}