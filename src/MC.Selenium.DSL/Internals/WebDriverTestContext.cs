using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Selenium.DSL
{
    public sealed class WebDriverTestContext : TestContext<IWebDriver> { }



    public class TestContext<T>
    {
        public T Item { get; set; }
        public ITestEventObserver Logger { get; set; }

        public static implicit operator T(TestContext<T> x)
        {
            return x.Item;
        }


    }

    public static class TestContext
    {
        public static TestContext<T> Build<T>(this T item, ITestEventObserver logger)
        {
            return new TestContext<T>
            {
                 Item = item,
                 Logger = logger
            };
        }

        public static TestContext<U> Pass<T, U>(this TestContext<T> from, Func<T, U> map)
        {
            return new TestContext<U>
            {
                Item = map(from.Item),
                Logger = from.Logger
            };
        }
    }
}
