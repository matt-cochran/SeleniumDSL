using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Selenium.DSL
{
    class WebElementCommand:Command
    {
        public By _By;
        public Action<IWebElement> _Action;

        /// <summary>
        /// Initializes a new instance of the WebElementCommand class.
        /// </summary>
        /// <param name="_By"></param>
        /// <param name="_Action"></param>
        public WebElementCommand(By _By, Action<IWebElement> _Action)
        {
            this._By = _By;
            this._Action = _Action;
        }


        public override void ExecuteWith(OpenQA.Selenium.IWebDriver driver)
        {
            _Action(driver.FindElement(_By));
        }
    }
}
