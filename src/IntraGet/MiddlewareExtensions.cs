using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Net;
using System.Web.Http;
using Microsoft.Owin.Hosting;

namespace IntraGet
{
    public static class MiddlewareExtensions
    {
        public static IAppBuilder EnableWindowsAuthentication(this IAppBuilder app)
        {
            HttpListener listener =
                (HttpListener)app.Properties["System.Net.HttpListener"];
            listener.AuthenticationSchemes =
                AuthenticationSchemes.IntegratedWindowsAuthentication;
            return app;
        }
        public static IAppBuilder UseAuthenticationMiddleware(this IAppBuilder app, IAuthenticationService authService)
        {
            var m = new IntraGetAuthenticationMiddleware(authService);
            app.Use(m);
            return app;
        }
        public static IAppBuilder UseNuGetServer(this IAppBuilder app)
        {
            var config = new HttpConfiguration();
            new NuGetBootstrap().Configure(config);
            app.UseWebApi(config);
            return app;
        }
    }
}
