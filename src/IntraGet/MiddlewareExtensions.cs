using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Net;

namespace IntraGet
{
    public static class MiddlewareExtensions
    {
        public static IAppBuilder UseAuthenticationMiddleware(this IAppBuilder app, IAuthenticationService authService)
        {
            var m = new IntraGetAuthenticationMiddleware(authService);
            app.Use(m);
            return app;
        }
    }
}
