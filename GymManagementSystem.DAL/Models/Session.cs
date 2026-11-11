namespace GymManagementSystem.DAL.Models
{
    public class Session : BaseEntity
    {
        public string Description { get; set; } = default!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //one to many relationship with trainer
        public Trainer Trainer { get; set; } = default!;

        public int TrainerId { get; set; } // no need to add it and known by convention that fk
        // but no need to add it as it would be able to add it alone ! , but we add it to be able to catch it in code


        // one to many relationship with category
        public Category Category { get; set; } = default!;
        public int CategoryId { get; set; }

        // one to many relationship with MemberSession
        public ICollection<MemberSession> MemberSessions { get; set; } = default!;


    }
}
