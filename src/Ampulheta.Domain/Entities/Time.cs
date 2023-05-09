namespace Ampulheta.Domain.Entities
{
    public class Time : EntityBase
    {
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
