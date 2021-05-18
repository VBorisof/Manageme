using System.Threading.Tasks;
using Manageme.Data.Models;

namespace Manageme.Data
{
    public interface IUnitOfWork
    {
        public IRepository<User> Users { get; }
        public IRepository<Reminder> Reminders { get; }
        public IRepository<Category> Categories { get; }

        void Save();
        Task SaveAsync();
    }
}
