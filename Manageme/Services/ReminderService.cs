using Manageme.Data;

namespace Manageme.Services
{
    public class ReminderService
    {
        private IUnitOfWork _unitOfWork;

        public ReminderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}

