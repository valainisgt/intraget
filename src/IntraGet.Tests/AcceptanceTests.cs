using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Owin.Testing;
using Moq;

namespace IntraGet.Tests
{
    public class AcceptanceTests
    {
        [Fact]
        public void GetReturnsResponse()
        {
            using (var sut = TestServer.Create((app) => app.UseAuthenticationMiddleware(new Mock<IAuthenticationService>().Object)))
            {
                var response = sut.HttpClient.GetAsync("/").Result;
                Assert.IsType<System.Net.Http.HttpResponseMessage>(response);
            }
        }
        [Fact]
        public void GetNuGetReturnsSuccess()
        {
            using(var sut = TestServer.Create((app) => app.UseNuGetServer()))
            {
                var response = sut.HttpClient.GetAsync("/nuget").Result;
                Assert.True(response.IsSuccessStatusCode);
            }
        }
        [Fact]
        [UseNuGetRepository]
        public void PushNuGetPackageSucceeds()
        {
            using (var sut = Microsoft.Owin.Hosting.WebApp.Start("http://localhost:5000/", (app) => app.UseNuGetServer()))
            {
                var nugetcli = new System.Diagnostics.ProcessStartInfo();
                nugetcli.FileName = "nuget.exe";
                nugetcli.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                nugetcli.Arguments = $"push MyNuGetPackage.1.0.0.nupkg -source http://localhost:5000/nuget";
                using (var exeProcess = System.Diagnostics.Process.Start(nugetcli))
                {
                    exeProcess.WaitForExit();
                }

                var packageDir = new System.IO.FileInfo(System.IO.Path.Combine("NuGetRepository", "MyNuGetPackage", "1.0.0", "MyNuGetPackage.1.0.0.nupkg"));
                Assert.True(packageDir.Exists);
            }
        }
    }
}
