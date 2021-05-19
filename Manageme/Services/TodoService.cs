using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manageme.Data;
using Manageme.Services.Shared;
using Manageme.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Manageme.Services
{
    public class TodoService
    {
        private IUnitOfWork _unitOfWork;

        public TodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ServiceResult<List<TodoViewModel>>
            GetTodos(long userId, long categoryId)
        {
            var todos = _unitOfWork.TaskItems.GetAsQueryable()
                .Include(r => r.User)
                .Include(r => r.Category)
                .Where(r => 
                        r.UserId == userId 
                        && r.CategoryId == categoryId
                        && r.Time == null); // TODOs have `null` time.

            var result = 
                todos.Select(r => new TodoViewModel(r)).ToList();

            return ServiceResult.Ok(result);
        }

        public async Task<ServiceResult> ToggleTodoDoneAsync(long userId, long todoId)
        {
            var todo = _unitOfWork.TaskItems.GetAsQueryable()
                .SingleOrDefault(ti => ti.Id == todoId && ti.UserId == userId);

            if (todo == null)
            {
                return ServiceResult.NotFound("Todo not found.");
            }

            todo.IsDone = !todo.IsDone;

            await _unitOfWork.SaveAsync();

            return ServiceResult.Ok();
        }

        public async Task<ServiceResult> DeleteTodoAsync(long userId, long todoId)
        {
            var todo = _unitOfWork.TaskItems.GetAsQueryable()
                .SingleOrDefault(ti => ti.Id == todoId && ti.UserId == userId);

            if (todo == null)
            {
                return ServiceResult.NotFound("Todo not found.");
            }

            _unitOfWork.TaskItems.Remove(todoId);

            await _unitOfWork.SaveAsync();

            return ServiceResult.Ok();
        }
    }
}

