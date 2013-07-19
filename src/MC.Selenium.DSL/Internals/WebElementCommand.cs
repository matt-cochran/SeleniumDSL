using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Selenium.DSL
{
    class WebElementCommand : Command
    {
        private readonly TestElement _Element;
        private readonly TestAction<IWebElement> _Action;

        /// <summary>
        /// Initializes a new instance of the WebElementCommand class.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        public WebElementCommand(TestElement element, TestAction<IWebElement> action)
        {
            this._Element = element;
            this._Action = action;
        }


        public override void ExecuteWith(IWebDriver driver)
        {
            driver.TryLog(TestEventType.Message, _Action.ActionName + " " + _Element.Name);
            _Action.Action(driver.FindElement(_Element.Finder));
        }
    }
}
