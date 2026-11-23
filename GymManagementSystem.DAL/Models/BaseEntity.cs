namespace GymManagementSystem.DAL.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; } // by default identity so no need to insert
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


    }
}
