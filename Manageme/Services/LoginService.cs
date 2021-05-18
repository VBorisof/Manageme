using System;
using System.Linq;
using System.Text;
using Manageme.Data;
using Manageme.Forms;
using Manageme.Services.Shared;
using Manageme.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Manageme.Services
{
    public class LoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public LoginService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        public ServiceResult<LoginViewModel> Login(LoginForm form)
        {
            if (string.IsNullOrWhiteSpace(form.Password)
                || string.IsNullOrWhiteSpace(form.Name))
            {
                return ServiceResult.BadRequest<LoginViewModel>(
                    "Username and Password are both required."
                );
            }

            var user = _unitOfWork.Users
                .GetAsQueryable()
                .SingleOrDefault(u => u.Name == form.Name);

            if (user == null)
            {
                return ServiceResult.InvalidPassword<LoginViewModel>(
                    "Login/Password pair is invalid."
                );
            }
            
            if (!PasswordHasher
                    .VerifyPasswordHash(
                        form.Password,
                        user.PasswordHash,
                        user.PasswordSalt))   
            {
                return ServiceResult.InvalidPassword<LoginViewModel>(
                    "Login/Password pair is invalid."
                );
            }

            var token = JwtTokenGenerator.GetTokenString(
                user,
                TimeSpan.FromDays(7),
                Encoding.ASCII.GetBytes(_configuration["AppSecret"])
            );

            var userViewModel = new UserViewModel(user);

            return ServiceResult.Ok(new LoginViewModel(token, userViewModel));
        }
    }
}
