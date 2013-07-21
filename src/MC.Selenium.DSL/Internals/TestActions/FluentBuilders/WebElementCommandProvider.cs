using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class WebElementCommandProvider
    {
        private readonly IWebElementDriver _op = new WebElementDriver();

        private static Action<T> GetAction<T>(Action<T> f)
        {
            return f;
        }

        public TestAction<IWebElement> Check()
        {
            const string name = "check";
            var f = GetAction<IWebElement>(_op.Check);            
            return TestAction.Create(f, name); ; 
        }

        public TestAction<IWebElement> Clear()
        {
            const string name = "clear";
            var f = GetAction<IWebElement>(_op.Clear);
            return TestAction.Create(f, name); 
        }

        public TestAction<IWebElement> Click()
        {
            const string name = "click";
            var f = GetAction<IWebElement>(_op.Click);
            return TestAction.Create(f, name);
        }

        public TestAction<IWebElement> Empty()
        {
            const string name = "";
            var f = GetAction<IWebElement>(_op.DoNothing);
            return TestAction.Create(f, name);
        }

        public TestAction<IWebElement> Select()
        {
            const string name = "select";
            var f = GetAction<IWebElement>(_op.Select);
            return TestAction.Create(f, name);
        }

        public TestAction<IWebElement> UnCheck()
        {
            const string name = "uncheck";
            var f = GetAction<IWebElement>(_op.Uncheck);
            return TestAction.Create(f, name);
        }

        public TestAction<IWebElement> UnSelect()
        {
            const string name = "select";
            var f = GetAction<IWebElement>(_op.Unselect);
            return TestAction.Create(f, name);
        }

        public TestAction<IWebElement> SendKeys(String value)
        {
            var f = GetAction<IWebElement>(_ => _op.SendKeys(_, value));
            return TestAction.Create(f, String.Format("sending keys '{0}' to", value));
        }

        public TestAction<IWebElement> SetValue(string value)
        {
            return TestAction.WebElement().Clear().Then(TestAction.WebElement().SendKeys(value));
        }

        public TestFunc<IWebElement, Boolean> GetAndTest(TestFunc<IWebElement, string> doGet, TestFunc<string, Boolean> doTest)
        {
            return new TestFunc<IWebElement, Boolean>()
            {
                Function = _ => doTest.Function(doGet.Function(_)),
                FunctionName = doGet.FunctionName + doTest.FunctionName
            };
        }

        private static readonly WebElementFuncProvider _CheckValue = new WebElementFuncProvider();

        public WebElementFuncProvider CheckValue()
        {
            return _CheckValue;
        }
    }
}
