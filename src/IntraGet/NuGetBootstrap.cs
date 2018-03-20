using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using NuGet.Server.V2;

namespace IntraGet
{
    public class NuGetBootstrap
    {
        public void Configure(HttpConfiguration config)
        {
            NuGetV2WebApiEnabler.UseNuGetV2WebApiFeed(config,
                routeName: "NuGet",
                routeUrlRoot: "NuGet",
                oDatacontrollerName: "NuGetGateway");
            config.Services.Replace(typeof(IHttpControllerActivator), new NuGetCompositionRoot());
        }
    }
}
