using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace IntraGet.Tests
{
    public class UseNuGetRepositoryAttribute : BeforeAfterTestAttribute
    {
        public override void Before(MethodInfo methodUnderTest)
        {
            System.IO.Directory.CreateDirectory("NuGetRepository");
            base.Before(methodUnderTest);
        }
        public override void After(MethodInfo methodUnderTest)
        {
            base.After(methodUnderTest);
            System.IO.Directory.Delete("NuGetRepository", true);
        }
    }
}
