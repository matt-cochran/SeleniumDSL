using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class StringTestAction
    {
        private static readonly StringAssertTestAction _Assert = new StringAssertTestAction();
        public StringAssertTestAction Assert()
        {
            return _Assert;
        }
        private static readonly TestAction<String> _Empty = TestAction.Create(new Action<String>(_ => { }), "");


        internal TestAction<String> Empty()
        {
            return _Empty;
        }
    }
}
