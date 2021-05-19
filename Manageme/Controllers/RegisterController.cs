using System.Net;
using System.Threading.Tasks;
using Manageme.Extensions;
using Manageme.Forms;
using Manageme.Services;
using Manageme.Services.Shared;
using Manageme.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Manageme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly RegistrationService _registrationService;

        public RegisterController(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }


        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(UserViewModel))]
        [SwaggerResponse((int) HttpStatusCode.Conflict, "User Exists", typeof(ErrorResult))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, "Bad Request", typeof(ErrorResult))]
        public async Task<IActionResult> Post([FromBody] RegistrationForm form)
        {
            return this.FromServiceResult(
                await _registrationService.RegisterUserAsync(form)
            );
        }
    }
}

