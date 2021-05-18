using System.Net;

namespace Manageme.Extensions
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccessful(this HttpStatusCode statusCode)
        {
            return ((int) statusCode >= 200) && ((int) statusCode <= 299);
        }
    }
}