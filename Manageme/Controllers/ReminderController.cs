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
        private readonly TaskItemService _taskItemService;

        public ReminderController(TaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }


        [Authorize]
        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(ReminderViewModel))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "User Not Found", typeof(ErrorResult))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, "Bad Request", typeof(ErrorResult))]
        public async Task<IActionResult> AddReminderAsync([FromBody] ReminderForm form)
        {
            return this.FromServiceResult(
                await _taskItemService.AddReminderAsync(this.GetRequestUserId(), form)
            );
        }
    }
}

