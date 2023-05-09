namespace Ampulheta.Domain.Entities
{
    public class User : EntityBase
    {
        public int UserTypeId { get;  set; }
        public UserType UserType { get; set; }

        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Time> Times { get; set; }

    }
}
