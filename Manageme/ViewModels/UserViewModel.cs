using Manageme.Data.Models;

namespace Manageme.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
        }
    }
}

