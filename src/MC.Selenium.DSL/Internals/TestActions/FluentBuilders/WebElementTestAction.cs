using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class WebElementTestAction
    {
        private static readonly TestAction<IWebElement> _Clear = TestAction.Create(Do.Clear, "clearing");
        private static readonly TestAction<IWebElement> _Click = TestAction.Create(Do.Click, "clicking");
        private static readonly TestAction<IWebElement> _Check = TestAction.Create(Do.Check, "checking");
        private static readonly TestAction<IWebElement> _Empty = TestAction.Create(Do.Nothing, String.Empty);
        private static readonly TestAction<IWebElement> _Select = TestAction.Create(Do.Select, "selecting");
        private static readonly TestAction<IWebElement> _UnCheck = TestAction.Create(Do.UnCheck, "unchecking");
        private static readonly TestAction<IWebElement> _UnSelect = TestAction.Create(Do.UnSelect, "unselecting");

        private static readonly TestActionAssert _Assert = new TestActionAssert();

        public TestActionAssert Assert()
        {
            return _Assert;
        }

        public TestAction<IWebElement> Check() { return _Check; }
        public TestAction<IWebElement> Clear() { return _Clear; }
        public TestAction<IWebElement> Click() { return _Click; }
        public TestAction<IWebElement> Empty() { return _Empty; }
        public TestAction<IWebElement> Select() { return _Select; }
        public TestAction<IWebElement> UnCheck() { return _UnCheck; }
        public TestAction<IWebElement> UnSelect() { return _UnSelect; }

        public TestAction<IWebElement> SendKeys(String value)
        {
            return TestAction.Create(Do.SendKeys(value), String.Format("sending keys '{0}' to", value));
        }

        internal TestAction<IWebElement> SetValue(string value)
        {
            return TestAction.WebElement().Clear().Then(TestAction.WebElement().SendKeys(value));
        }

        internal TestAction<IWebElement> GetAndTest(TestFunc<IWebElement, string> doGet, TestAction<string> doTest)
        {
            return TestAction.Create(new Action<IWebElement>(_ => doTest.Action(doGet.Function(_))), doGet.FunctionName + doTest.ActionName);
        }
    }
}
