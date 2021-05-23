using System;
using Manageme.Data.Models;

namespace Manageme.ViewModels
{
    public class ReminderViewModel
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public long CategoryId { get; set; }
        public DateTime? Time { get; set; }

        public ReminderViewModel(TaskItem taskItem)
        {
            Id = taskItem.Id;
            Content = taskItem.Content;
            CategoryId = taskItem.CategoryId;
            Time = taskItem.Time;
        }
    }
}

