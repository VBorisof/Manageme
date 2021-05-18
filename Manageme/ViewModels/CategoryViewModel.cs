using Manageme.Data.Models;

namespace Manageme.ViewModels
{
    public class CategoryViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public CategoryViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }
    } 
}

