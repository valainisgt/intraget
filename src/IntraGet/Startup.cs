using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Net;

[assembly: OwinStartup(typeof(IntraGet.Startup))]

namespace IntraGet
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureHttpListener(app);
            ConfigureMiddleware(app);
        }

        public static void ConfigureHttpListener(IAppBuilder app)
        {
            HttpListener listener =
                   (HttpListener)app.Properties["System.Net.HttpListener"];
            listener.AuthenticationSchemes =
                AuthenticationSchemes.IntegratedWindowsAuthentication;
        }

        public static void ConfigureMiddleware(IAppBuilder app)
        {
            app.Use<IntraGetAuthenticationMiddleware>();
            app.Run(async (ctx) =>
            {
                await ctx.Response.WriteAsync("hello world!");
            });
        }
    }
}
