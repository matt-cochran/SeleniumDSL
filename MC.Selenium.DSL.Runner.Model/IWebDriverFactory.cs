using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Selenium.DSL.Runner.Model
{
    internal interface IWebDriverFactory
    {
        IWebDriver Build(String name);
    }
}
