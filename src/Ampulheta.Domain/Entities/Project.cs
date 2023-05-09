namespace Ampulheta.Domain.Entities
{
    public class Project : EntityBase
    {
        public string Name { get;  set; }
        public string Note { get;  set; }

        public ICollection<Time> Times { get; set; }

    }
}
