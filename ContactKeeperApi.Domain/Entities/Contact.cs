using ContactKeeperApi.Domain.Enum;

namespace ContactKeeperApi.Domain.Entities
{
    public class Contact : Entity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public EContactType Type { get; set; }
    }
}
