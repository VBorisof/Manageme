using System.Linq;
using System.Threading.Tasks;
using Manageme.Data;
using Manageme.Data.Models;
using Manageme.Forms;
using Manageme.Services.Shared;
using Manageme.ViewModels;

namespace Manageme.Services
{
    public class RegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        
        public async Task<ServiceResult<UserViewModel>> RegisterUserAsync(RegistrationForm form)
        {
            if (string.IsNullOrEmpty(form.Name)
                || string.IsNullOrEmpty(form.Password))
            {
                return ServiceResult.BadRequest<UserViewModel>(
                    "Email and password are both required!"
                );
            }

            var userExists = _unitOfWork.Users
                .GetAsQueryable()
                .Any(au => au.Name == form.Name);

            if (userExists)
            {
                return ServiceResult.Conflict<UserViewModel>(
                    $"User {form.Name} already exists"
                );
            }

            PasswordHasher.CreatePasswordHash(
                form.Password,
                out var passwordHash,
                out var passwordSalt
            );

            var user = new User(form.Name, passwordHash, passwordSalt);
            
            user = _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveAsync();

            return ServiceResult.Ok(new UserViewModel(user));
        }
    }
}

