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
        private static void SetupServer(Owin.IAppBuilder app)
        {
            IntraGet.Startup.ConfigureMiddleware(app);
        }
        [Fact]
        public void GetReturnsResponse()
        {
            using (var sut = TestServer.Create(SetupServer))
            {
                var response = sut.HttpClient.GetAsync("/").Result;

                Assert.IsType<System.Net.Http.HttpResponseMessage>(response);
            }
        }
        [Fact]
        public void GetReturnsFailureWithBadCredentials()
        {
            var authServiceStub = new Mock<IntraGet.IIntraGetAuthenticationService>();
            authServiceStub.Setup(s => s.Authenticate(It.IsAny<System.Security.Principal.IPrincipal>())).Returns(false);
            using (var sut = TestServer.Create(SetupServer))
            {
                var response = sut.HttpClient.GetAsync("/").Result;
                Assert.False(response.IsSuccessStatusCode);
            }
        }
        [Fact]
        public void GetReturnsSuccessWithGoodCredentials()
        {
            var authServiceStub = new Mock<IntraGet.IIntraGetAuthenticationService>();
            authServiceStub.Setup(s => s.Authenticate(It.IsAny<System.Security.Principal.IPrincipal>())).Returns(true);
            using (var sut = TestServer.Create(SetupServer))
            {
                var response = sut.HttpClient.GetAsync("/").Result;
                Assert.True(response.IsSuccessStatusCode);
            }
        }
    }
}
