using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manageme.Data;
using Manageme.Data.Models;
using Manageme.Extensions;
using Manageme.Forms;
using Manageme.Services.Shared;
using Manageme.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Manageme.Services
{
    public class ReminderService
    {
        private IUnitOfWork _unitOfWork;

        public ReminderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ServiceResult<ReminderViewModel>>
            AddTaskItemAsync (long userId, ReminderForm form)
        {
            if (form.CategoryId == null 
                || string.IsNullOrWhiteSpace(form.Content))
            {
                return ServiceResult.BadRequest<ReminderViewModel>(
                    "CategoryId and Content are both required."
                );
            }

            if (!_unitOfWork.Users.Any(u => u.Id == userId))
            {
                return ServiceResult.NotFound<ReminderViewModel>("User not found.");
            }

            var reminder = new Reminder(userId, form.CategoryId.Value, form.Content, form.Time);

            reminder = await _unitOfWork.Reminders.AddAsync(reminder);

            await _unitOfWork.SaveAsync();

            // Fetch the reminder from the DB to get it with category.
            // TODO: There's probably a better way of doing this...
            reminder = 
                await _unitOfWork.Reminders.GetAsQueryable()
                    .Include(r => r.Category)
                    .SingleOrDefaultAsync(r => r.Id == reminder.Id);

            return ServiceResult.Ok(new ReminderViewModel(reminder));
        }

        public ServiceResult<List<ReminderViewModel>>
            GetTodos(long userId, long categoryId)
        {
            var reminders = _unitOfWork.Reminders.GetAsQueryable()
                .Include(r => r.User)
                .Include(r => r.Category)
                .Where(r => 
                        r.UserId == userId 
                        && r.CategoryId == categoryId
                        && r.Time == null); // TODOs have `null` time.

            if (reminders.None())
            {
                return ServiceResult.NotFound<List<ReminderViewModel>>(
                    "User reminders under this category not found."
                );
            }

            var result = 
                reminders.Select(r => new ReminderViewModel(r)).ToList();

            return ServiceResult.Ok(result);
        }
    }
}

