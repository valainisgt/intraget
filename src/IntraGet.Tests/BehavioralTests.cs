using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Owin.Testing;
using IntraGet;
using System.Security.Principal;
using Microsoft.Owin;
using Owin;

namespace IntraGet.Tests
{
    public class BehavioralTests
    {
        [Fact]
        public void GetReturnsFailureWithBadCredentials()
        {
            var stub = new Mock<IAuthenticationService>();
            stub.Setup(s => s.Authenticate(It.IsAny<IPrincipal>())).Returns(false);
            using (var sut = TestServer.Create((app) => app.UseAuthenticationMiddleware(stub.Object)))
            {
                var resp = sut.HttpClient.GetAsync("/").Result;
                Assert.False(resp.IsSuccessStatusCode);
                Assert.Equal(System.Net.HttpStatusCode.Unauthorized, resp.StatusCode);
                stub.Verify(s => s.Authenticate(It.IsAny<IPrincipal>()), Times.Once);
            }
        }
        [Fact]
        public void GetReturnsSuccessWithGoodCredentials()
        {
            var stubAuthService = new Mock<IAuthenticationService>();
            stubAuthService.Setup(s => s.Authenticate(It.IsAny<IPrincipal>())).Returns(true);
            var stubMiddleware = new Mock<Func<IOwinContext, Task>>();
            using (var sut = TestServer.Create((app) => app.UseAuthenticationMiddleware(stubAuthService.Object).Run(stubMiddleware.Object)))
            {
                var resp = sut.HttpClient.GetAsync("/").Result;
                Assert.True(resp.IsSuccessStatusCode);
                stubAuthService.Verify(s => s.Authenticate(It.IsAny<IPrincipal>()), Times.Once);
            }
        }
    }
}
