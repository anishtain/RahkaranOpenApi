using Microsoft.Extensions.DependencyInjection;
using RahkaranOpenApi.Data.AuthenticationServices;
using RahkaranOpenApi.Data.Commons;
using RahkaranOpenApi.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RahkaranOpenApi.Data
{
    public static class Startup
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomeRestRequest, RestSharpConfig>();
            services.AddScoped<IAuthentication, AuthenticationService>();
            return services;
        }
    }
}
