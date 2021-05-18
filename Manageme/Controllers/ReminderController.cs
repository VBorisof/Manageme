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
    public class ReminderController : ControllerBase
    {
        private readonly ReminderService _reminderService;

        public ReminderController(ReminderService reminderService)
        {
            _reminderService = reminderService;
        }


        [Authorize]
        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(ReminderViewModel))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "User Not Found", typeof(ErrorResult))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, "Bad Request", typeof(ErrorResult))]
        public async Task<IActionResult> Post([FromBody] ReminderForm form)
        {
            return this.FromServiceResult(
                await _reminderService.AddTaskItemAsync(this.GetRequestUserId(), form)
            );
        }
        
        [Authorize]
        [HttpGet("todo/{categoryId}")]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(ReminderViewModel))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "TODOs Not Found", typeof(ErrorResult))]
        public IActionResult GetTodos(long categoryId)
        {
            return this.FromServiceResult(
                _reminderService.GetTodos(this.GetRequestUserId(), categoryId)
            );
        }
    }
}

