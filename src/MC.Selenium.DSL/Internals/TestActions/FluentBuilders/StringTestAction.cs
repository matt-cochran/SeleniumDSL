using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class StringTestAction
    {
        private static readonly TestAction<String> _Empty = TestAction.Create(new Action<String>(_ => { }), "");

        internal TestAction<String> Empty()
        {
            return _Empty;
        }

        private static readonly StringPredicate _Check = new StringPredicate();

        internal StringPredicate Check()
        {
            return _Check;
        }
    }
}
