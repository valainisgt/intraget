using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGet.Server.Core.Infrastructure;
using NuGet.Server.V2.Controllers;
using NuGet.Server.V2;

namespace IntraGet
{
    public class NuGetGatewayController : NuGetODataController
    {
        public NuGetGatewayController() : 
            base(NuGetV2WebApiEnabler.CreatePackageRepository("NuGetRepository"), new ApiKeyPackageAuthenticationService(false, null)) { }
    }
}
