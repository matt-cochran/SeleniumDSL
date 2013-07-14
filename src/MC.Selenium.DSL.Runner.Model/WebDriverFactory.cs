using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Selenium.DSL.Runner.Model
{
    internal class WebDriverFactory : IWebDriverFactory
    {
        public IWebDriver Build(String name)
        {
            switch (name)
            {
                case "firefox": return new FirefoxDriver();
                case "ie": return new InternetExplorerDriver();
                case "chrome": return new ChromeDriver();
                default:
                    throw new NotSupportedException("unrecognized driver: " + name);
            }
        }
    }
}
