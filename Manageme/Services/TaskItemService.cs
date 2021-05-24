using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manageme.Data;
using Manageme.Data.Models;
using Manageme.Forms;
using Manageme.Services.Shared;
using Manageme.ViewModels;

namespace Manageme.Services
{
    public class TaskItemService
    {
        private IUnitOfWork _unitOfWork;

        public TaskItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<ServiceResult<TodoViewModel>>
            AddTodoAsync (long userId, TodoForm form)
        {
            if (form.CategoryId == null 
                || string.IsNullOrWhiteSpace(form.Content))
            {
                return ServiceResult.BadRequest<TodoViewModel>(
                    "CategoryId and Content are required."
                );
            }

            if (!_unitOfWork.Users.Any(u => u.Id == userId))
            {
                return ServiceResult.NotFound<TodoViewModel>("User not found.");
            }

            var todo = 
                new TaskItem(
                    userId,
                    form.CategoryId.Value,
                    form.Content,
                    time: null
                );

            todo = await _unitOfWork.TaskItems.AddAsync(todo);

            await _unitOfWork.SaveAsync();

            return ServiceResult.Ok(new TodoViewModel(todo));
        }

        public async Task<ServiceResult<ReminderViewModel>>
            AddReminderAsync (long userId, ReminderForm form)
        {
            if (form.CategoryId == null 
                || string.IsNullOrWhiteSpace(form.Content)
                || form.Time == null)
            {
                return ServiceResult.BadRequest<ReminderViewModel>(
                    "CategoryId and Content and Time are all required."
                );
            }

            if (!_unitOfWork.Users.Any(u => u.Id == userId))
            {
                return ServiceResult.NotFound<ReminderViewModel>("User not found.");
            }

            var reminder = new TaskItem(userId, form.CategoryId.Value, form.Content, form.Time);

            reminder = await _unitOfWork.TaskItems.AddAsync(reminder);

            await _unitOfWork.SaveAsync();

            return ServiceResult.Ok(new ReminderViewModel(reminder));
        }
    }
}

