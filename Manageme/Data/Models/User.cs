using System.Collections.Generic;

namespace Manageme.Data.Models
{
    public class User : ModelBase
    {
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<Reminder> Reminders { get; set; }
        public ICollection<Category> Categories { get; set; }

        public User(string name, byte[] passwordHash, byte[] passwordSalt)
        {
            Name = name;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;

            Reminders = new HashSet<Reminder>();
            Categories = new HashSet<Category>();
        }
        public User() { }
    }   
}
