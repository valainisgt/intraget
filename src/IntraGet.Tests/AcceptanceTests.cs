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
    }
}
