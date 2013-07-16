using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class WebElementTestAction
    {
        private static readonly TestAction<IWebElement> _Clear = TestAction.Create(Get.Clear, "clearing");
        private static readonly TestAction<IWebElement> _Click = TestAction.Create(Get.Click, "clicking");
        private static readonly TestAction<IWebElement> _Check = TestAction.Create(Get.Check, "checking");
        private static readonly TestAction<IWebElement> _Empty = TestAction.Create(Get.Nothing, String.Empty);
        private static readonly TestAction<IWebElement> _Select = TestAction.Create(Get.Select, "selecting");
        private static readonly TestAction<IWebElement> _UnCheck = TestAction.Create(Get.UnCheck, "unchecking");
        private static readonly TestAction<IWebElement> _UnSelect = TestAction.Create(Get.UnSelect, "unselecting");

        public TestAction<IWebElement> Check() { return _Check; }
        public TestAction<IWebElement> Clear() { return _Clear; }
        public TestAction<IWebElement> Click() { return _Click; }
        public TestAction<IWebElement> Empty() { return _Empty; }
        public TestAction<IWebElement> Select() { return _Select; }
        public TestAction<IWebElement> UnCheck() { return _UnCheck; }
        public TestAction<IWebElement> UnSelect() { return _UnSelect; }

        public TestAction<IWebElement> SendKeys(String value)
        {
            return TestAction.Create(Get.SendKeys(value), String.Format("sending keys '{0}' to", value));
        }

        internal TestAction<IWebElement> SetValue(string value)
        {
            return TestAction.WebElement().Clear().Then(TestAction.WebElement().SendKeys(value));
        }

        internal TestFunc<IWebElement, Boolean> GetAndTest(TestFunc<IWebElement, string> doGet, TestFunc<string, Boolean> doTest)
        {
            return new TestFunc<IWebElement, Boolean>()
            {
                Function = _ => doTest.Function(doGet.Function(_)),
                FunctionName = doGet.FunctionName + doTest.FunctionName
            };
        }

        private static readonly WebElementPredicate _CheckValue = new WebElementPredicate();

        public WebElementPredicate CheckValue()
        {
            return _CheckValue;
        }
    }
}
