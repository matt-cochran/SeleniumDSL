using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Selenium.DSL
{
    internal abstract class Command
    {
        public abstract void ExecuteWith(IWebDriver driver);
    }

    
}
