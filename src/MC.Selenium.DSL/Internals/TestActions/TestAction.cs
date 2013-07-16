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

        private static readonly WebElementTestAction _WebElement = new WebElementTestAction();
        private static readonly StringTestAction _StringAction = new StringTestAction();

        public static StringTestAction String() { return _StringAction; }
        public static WebElementTestAction WebElement() { return _WebElement; }

        internal static TestFunc<IWebElement, String> HasAttribute(string value)
        {
            return TestFunc.Create(new Func<IWebElement, String>(_ => _.GetAttribute(value)), "has attribute '" + value + "'");
        }

        internal static TestFunc<IWebElement, String> HasCssKey(string quoted)
        {
            return TestFunc.Create(new Func<IWebElement, String>(_ => _.GetCssValue(quoted)), "asserting has css key '" + quoted + "'");
        }

        internal static TestFunc<IWebElement, String> HasText()
        {
            return TestFunc.Create(new Func<IWebElement, String>(_ => _.Text), "asserting has text");
        }
    }
}