using System.Collections.Generic;

namespace ContactKeeperApi.Domain.Entities
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
