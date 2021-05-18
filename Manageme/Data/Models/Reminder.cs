using System;

namespace Manageme.Data.Models
{
    public class Reminder : ModelBase
    {
        public string Content { get; set; }
        public DateTime? Time { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public Reminder(
            long userId,
            long categoryId,
            string content,
            DateTime? time
        )
        {
            UserId = userId;
            CategoryId = categoryId;
            Content = content;
            Time = time;
        }
        public Reminder() { }
    }
}

