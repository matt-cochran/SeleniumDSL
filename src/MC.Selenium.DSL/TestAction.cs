using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class TestAction<T>
    {
        public Action<T> Action { get; set; }
        public String ActionName { get; set; }
    }

    internal static class TestAction
    {
        internal static TestAction<T> Create<T>(Action<T> action, string name)
        {
            return new TestAction<T>
            {
                ActionName = name,
                Action = action
            };
        }

        public static TestAction<IWebElement> SendKeys(String value)
        {
            return TestAction.Create(Do.SendKeys(value), String.Format("sending keys '{0}' to", value));
        }
        internal static readonly TestAction<IWebElement> AssertIsSelected = TestAction.Create(Do.AssertIsSelected, "asserting is selected");
        internal static readonly TestAction<IWebElement> AssertNotSelected = TestAction.Create(Do.AssertNotSelected, "asserting is not selected");
        internal static readonly TestAction<IWebElement> AssertNotChecked = TestAction.Create(Do.AssertNotChecked, "asserting not checked");
        internal static readonly TestAction<IWebElement> AssertIsChecked = TestAction.Create(Do.AssertIsChecked, "asserting is checked");
        internal static readonly TestAction<IWebElement> Clear = TestAction.Create(Do.Clear, "clearing");
        internal static readonly TestAction<IWebElement> Click = TestAction.Create(Do.Click, "clicking");
        internal static readonly TestAction<IWebElement> Check = TestAction.Create(Do.Check, "checking");
        internal static readonly TestAction<IWebElement> Select = TestAction.Create(Do.Select, "selecting");
        internal static readonly TestAction<IWebElement> UnCheck = TestAction.Create(Do.UnCheck, "unchecking");
        internal static readonly TestAction<IWebElement> UnSelect = TestAction.Create(Do.UnSelect, "unselecting");
    }
}