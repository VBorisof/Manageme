using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manageme.Data;
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

        public async Task<ServiceResult<List<ReminderViewModel>>>
            GetRemindersAsync (long userId)
        {
            var reminders = await _unitOfWork.TaskItems.GetAsQueryable()
                .Where(ti => ti.UserId == userId
                          && ti.Time != null
                          && ti.Time <= DateTime.UtcNow
                          && !ti.IsDone)
                .ToListAsync();
            
            var result = reminders.Select(r => new ReminderViewModel(r)).ToList();
            return ServiceResult.Ok(result);
        }

        public async Task<ServiceResult>
            AcknowledgeReminderAsync (long userId, long reminderId)
        {
            var reminder = await _unitOfWork.TaskItems.GetAsQueryable()
                .SingleOrDefaultAsync(ti => 
                    ti.UserId == userId && ti.Id == reminderId
                );
           
            if (reminder == null)
            {
                return ServiceResult.NotFound("Reminder not found.");
            }

            reminder.IsDone = true;

            await _unitOfWork.SaveAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult>
            SnoozeReminderAsync (long userId, SnoozeReminderForm form)
        {
            if (form.Time == null || form.Time <= DateTime.UtcNow)
            {
                return ServiceResult.BadRequest(
                    "`time`: Some date in future is required."
                );
            }

            var reminder = await _unitOfWork.TaskItems.GetAsQueryable()
                .SingleOrDefaultAsync(ti => 
                    ti.UserId == userId && ti.Id == form.Id
                );
           
            if (reminder == null)
            {
                return ServiceResult.NotFound("Reminder not found.");
            }

            reminder.Time = form.Time;

            await _unitOfWork.SaveAsync();

            return ServiceResult.Ok();
        }
    }
}

