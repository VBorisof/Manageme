using Microsoft.AspNetCore.Mvc;
using Manageme.Services.Shared;
using System.Linq;

namespace Manageme.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static long GetRequestUserId(this ControllerBase controllerBase)
        {
            var userId = controllerBase.HttpContext.User.Claims
                .Single(c => c.Type == ClaimTypes.UserId);

            return long.Parse(userId.Value);
        }

        public static ActionResult FromServiceResult<TData>(
            this ControllerBase controllerBase, ServiceResult<TData> result
        ) where TData : class
        {
            if (result.IsSuccessful)
            {
                return controllerBase.StatusCode((int) result.StatusCode, result.Data);
            }

            return controllerBase.StatusCode((int) result.StatusCode, new ErrorResult
            {
                Errors = result.Errors
            });
        }

        public static ActionResult FromServiceResult(
            this ControllerBase controllerBase, ServiceResult result
        )
        {
            if (result.IsSuccessful)
            {
                return controllerBase.StatusCode((int) result.StatusCode);
            }

            return controllerBase.StatusCode((int) result.StatusCode, new ErrorResult
            {
                Errors = result.Errors
            });
        }
    }
}
