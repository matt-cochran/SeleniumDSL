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

        internal static TestFunc<IWebElement, String> GetAttribute(string value)
        {
            return TestFunc.Create(new Func<IWebElement, String>(_ => _.GetAttribute(value)), "has attribute '" + value + "'");
        }

        internal static TestFunc<IWebElement, String> GetCssKey(string quoted)
        {
            return TestFunc.Create(new Func<IWebElement, String>(_ => _.GetCssValue(quoted)), "has css key '" + quoted + "'");
        }

        internal static TestFunc<IWebElement, String> GetText()
        {
            return TestFunc.Create(
                new Func<IWebElement, String>(_ =>
                    {
                        if (_.IsTextInput() ||
                           _.IsTextArea())
                        {
                            return _.GetAttribute("value");
                        }


                        return _.Text;
                    }),
                "has text");
        }
    }
}