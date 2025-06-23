using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace TestAutomationTask.Tests
{
    public class LoginTestsFirefox : LoginTestsBase
    {
        public LoginTestsFirefox(ITestOutputHelper output) : base(output, "Firefox") { }
    }
}
