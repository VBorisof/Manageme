using System;
using Manageme.Data.Models;

namespace Manageme.ViewModels
{
    public class ReminderViewModel
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public DateTime? Time { get; set; }

        public ReminderViewModel(TaskItem reminder)
        {
            Id = reminder.Id;
            Content = reminder.Content;
            Category = reminder.Category.Name;
            Time = reminder.Time;
        }
    }
}
