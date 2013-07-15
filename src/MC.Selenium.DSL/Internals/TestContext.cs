using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Selenium.DSL
{
    public sealed class TestContext
    {
        public IWebDriver WebDriver { get; set; }
        public ITestEventObserver Logger { get; set; }
    }
}
