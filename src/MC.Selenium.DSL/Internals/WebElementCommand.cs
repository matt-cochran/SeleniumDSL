using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Selenium.DSL
{
    class WebElementCommand : Command
    {
        private readonly By _By;
        private readonly TestAction<IWebElement> _Action;

        /// <summary>
        /// Initializes a new instance of the WebElementCommand class.
        /// </summary>
        /// <param name="_By"></param>
        /// <param name="_Action"></param>
        public WebElementCommand(By _By, TestAction<IWebElement> _Action)
        {
            this._By = _By;
            this._Action = _Action;
        }


        public override void ExecuteWith(TestContext context)
        {
            context.Logger.Log(TestEventType.Message, _Action.ActionName + " with ");
            _Action.Action(context.WebDriver.FindElement(_By));
        }
    }
}
