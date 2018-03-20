using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace IntraGet
{
    public class NuGetCompositionRoot : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            if(controllerType == typeof(NuGetGatewayController))
            {
                return new NuGetGatewayController();
            }
            else
            {
                var activator = new DefaultHttpControllerActivator();
                return activator.Create(request, controllerDescriptor, controllerType);
            }
        }
    }
}
