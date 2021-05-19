using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Manageme.Data.Models;

namespace Manageme.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        
        private Repository<User> _users;
        public IRepository<User> Users => _users ?? (_users = new Repository<User>(_context));
        
        private Repository<TaskItem> _taskItems;
        public IRepository<TaskItem> TaskItems => _taskItems ?? (_taskItems = new Repository<TaskItem>(_context));
       
        private Repository<Category> _categories;
        public IRepository<Category> Categories => _categories ?? (_categories = new Repository<Category>(_context));

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public void Save()
        {
            const int attemptCount = 3;

            int attemptsLeft = attemptCount;
            bool ok = false;

            while (--attemptsLeft > 0 && !ok)
            {
                try
                {
                    _context.SaveChanges();
                    ok = true;
                }
                catch
                {
                    Debug.WriteLine($"{nameof(UnitOfWork)}.{nameof(Save)}: Failed to save changes");
                }
            }

            if (!ok)
            {
                throw new Exception($"Could not save entity in {nameof(UnitOfWork)}.{nameof(Save)} after {attemptCount} retries.");
            }
        }
        
        public async Task SaveAsync()
        {
            const int attemptCount = 3;

            int attemptsLeft = attemptCount;
            bool ok = false;

            string lastError = "";
            while (--attemptsLeft > 0 && !ok)
            {
                try
                {
                    await _context.SaveChangesAsync();
                    ok = true;
                }
                catch (Exception e)
                {
                    lastError = e.ToString();
                    Debug.WriteLine($"{nameof(UnitOfWork)}.{nameof(SaveAsync)}: Failed to save changes: {lastError}");
                }
            }

            if (!ok)
            {
                throw new Exception($"Could not save entity in {nameof(UnitOfWork)}.{nameof(SaveAsync)} after {attemptCount} retries: {lastError}");
            }
        }
    }
}
