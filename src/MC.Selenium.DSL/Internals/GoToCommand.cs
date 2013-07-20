using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace MC.Selenium.DSL
{
    internal class GoToCommand : Command
    {
        private string _URL;

        public GoToCommand(string url)
        {
            this._URL = url;
        }

        public override void ExecuteWith(IWebDriver driver)
        {
            driver.TryLog(TestEventType.BeginCommand, "going to " + _URL);
            driver.Navigate().GoToUrl(_URL);
            driver.TryLog(TestEventType.EndCommand, "going to " + _URL);
        }

        public string URL { get { return _URL; } }
    }
}
