using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Selenium.DSL.Runner.Model.Tests
{
    internal class TestWebDriverFactory : IWebDriverFactory
    {
        public IWebDriver Build(String name)
        {
            return new PhantomJSDriver(PhantomJSDriverService.CreateDefaultService(Config.PhantomJsInstallPath), new PhantomJSOptions());
        }
    }
}
