using Manageme.Data.Models;

namespace Manageme.ViewModels
{
    public class TodoViewModel
    {
        public long Id { get; set; }
        public bool IsDone { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }

        public TodoViewModel(TaskItem taskItem)
        {
            Id = taskItem.Id;
            IsDone = taskItem.IsDone;
            Content = taskItem.Content;
            Category = taskItem.Category.Name;
        }
    }
}

