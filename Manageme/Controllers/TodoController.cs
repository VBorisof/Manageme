using System.Net;
using System.Threading.Tasks;
using Manageme.Extensions;
using Manageme.Forms;
using Manageme.Services;
using Manageme.Services.Shared;
using Manageme.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Manageme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;
        private readonly TaskItemService _taskItemService;

        public TodoController(TodoService todoService, TaskItemService taskItemService)
        {
            _todoService = todoService;
            _taskItemService = taskItemService;
        }


        [Authorize]
        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(TodoViewModel))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "User Not Found", typeof(ErrorResult))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, "Bad Request", typeof(ErrorResult))]
        public async Task<IActionResult> AddTodoAsync([FromBody] TodoForm form)
        {
            return this.FromServiceResult(
                await _taskItemService.AddTodoAsync(this.GetRequestUserId(), form)
            );
        }

        [Authorize]
        [HttpGet("{categoryId}")]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(TodoViewModel))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "TODOs Not Found", typeof(ErrorResult))]
        public IActionResult GetTodos(long categoryId)
        {
            return this.FromServiceResult(
                _todoService.GetTodos(this.GetRequestUserId(), categoryId)
            );
        }

        [Authorize]
        [HttpPut("{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(string))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "TODO Not Found", typeof(ErrorResult))]
        public async Task<IActionResult> ToggleTodoDoneAsync(long id)
        {
            return this.FromServiceResult(
                await _todoService.ToggleTodoDoneAsync(this.GetRequestUserId(), id)
            );
        }

        [Authorize]
        [HttpDelete("{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(string))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "TODO Not Found", typeof(ErrorResult))]
        public async Task<IActionResult> DeleteTodoAsync(long id)
        {
            return this.FromServiceResult(
                await _todoService.DeleteTodoAsync(this.GetRequestUserId(), id)
            );
        }
    }
}

