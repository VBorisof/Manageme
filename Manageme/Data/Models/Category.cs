namespace Manageme.Data.Models
{
    public class Category : ModelBase
    {
        public string Name { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public Category(string name, long userId)
        {
            Name = name;
            UserId = userId;
        }
        public Category() { }
    }
}

