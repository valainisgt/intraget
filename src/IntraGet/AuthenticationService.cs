using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.Net;
using System.Security.Principal;

namespace IntraGet
{
    public class IntraGetAuthenticationMiddleware : OwinMiddleware
    {
        private readonly IIntraGetAuthenticationService authService;
        public IntraGetAuthenticationMiddleware(OwinMiddleware next, IIntraGetAuthenticationService authService) : base(next)
        {
            if(authService == null)
            {
                throw new ArgumentNullException(nameof(authService));
            }
            this.authService = authService;
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (this.authService.Authenticate(context.Request.User))
            {
                await Next.Invoke(context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("401 Unauthorized");
            }
        }
    }
    public interface IIntraGetAuthenticationService
    {
        bool Authenticate(IPrincipal user);
    }
}
