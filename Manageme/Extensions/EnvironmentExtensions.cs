using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Manageme.Extensions
{
    public static class EnvironmentExtensions
    {
        public static bool IsDevelopment()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return environment == Environments.Development;
        }
    }
}