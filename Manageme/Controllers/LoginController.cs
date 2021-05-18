using System.Net;
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
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        [SwaggerResponse((int) HttpStatusCode.OK, "Okay", typeof(LoginViewModel))]
        [SwaggerResponse((int) HttpStatusCodeExtras.InvalidPassword, "Invalid Username/Password", typeof(ErrorResult))]
        [SwaggerResponse((int) HttpStatusCode.BadRequest, "Bad Request", typeof(ErrorResult))]
        public IActionResult Post([FromBody] LoginForm form)
        {
            return this.FromServiceResult(
                _loginService.Login(form)
            );
        }
    }
}
