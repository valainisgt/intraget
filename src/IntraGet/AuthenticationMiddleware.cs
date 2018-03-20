using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;

namespace IntraGet
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class IntraGetAuthenticationMiddleware
    {
        private readonly IAuthenticationService authService;
        private AppFunc next;
        public IntraGetAuthenticationMiddleware(IAuthenticationService authService)
        {
            if(authService == null)
            {
                throw new ArgumentNullException(nameof(authService));
            }
            this.authService = authService;
        }
        public void Initialize(AppFunc next)
        {
            this.next = next;
        }
        public async Task Invoke(IDictionary<string, object> env)
        {
            
            OwinContext context = new OwinContext(env);
            if (this.authService.Authenticate(context.Request.User))
            {
                await this.next.Invoke(env);
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("401 Unauthorized");
            }
        }
    }
    public interface IAuthenticationService
    {
        bool Authenticate(System.Security.Principal.IPrincipal user);
    }
}
