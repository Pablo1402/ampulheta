namespace Ampulheta.Domain.Entities
{
    public class UserType : EntityBase
    {
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
