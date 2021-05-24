using System.Collections.Generic;
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
        private readonly ReminderService _reminderService;

        public ReminderController(
            TaskItemService taskItemService,
            ReminderService reminderService
        )
        {
            _taskItemService = taskItemService;
            _reminderService = reminderService;
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
        
        [Authorize]
        [HttpGet]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(List<ReminderViewModel>))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, "Bad Request", typeof(ErrorResult))]
        public async Task<IActionResult> GetRemindersAsync()
        {
            return this.FromServiceResult(
                await _reminderService.GetRemindersAsync(this.GetRequestUserId())
            );
        }

        [Authorize]
        [HttpPut("{id}")]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(List<ReminderViewModel>))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "Reminder Not Found", typeof(ErrorResult))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, "Bad Request", typeof(ErrorResult))]
        public async Task<IActionResult> AcknowledgeReminderAsync(long id)
        {
            return this.FromServiceResult(
                await _reminderService.AcknowledgeReminderAsync(
                    this.GetRequestUserId(), id
                )
            );
        }
        
        [Authorize]
        [HttpPut]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(List<ReminderViewModel>))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, "Reminder Not Found", typeof(ErrorResult))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, "Bad Request", typeof(ErrorResult))]
        public async Task<IActionResult> SnoozeReminderAsync(SnoozeReminderForm form)
        {
            return this.FromServiceResult(
                await _reminderService.SnoozeReminderAsync(
                    this.GetRequestUserId(), form
                )
            );
        }
    }
}

