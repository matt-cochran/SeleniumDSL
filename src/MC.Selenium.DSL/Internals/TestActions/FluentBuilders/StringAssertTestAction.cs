using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class StringAssertTestAction
    {
        public TestAction<String> IsEqualTo(String value)
        {
            return TestAction.Create(
                new Action<String>(x => x.Equals(value)),
                String.Format("with value '{0}'", value));
        }


        internal TestAction<String> EndsWith(string value)
        {
            return TestAction.Create(new Action<String>(_ => _.EndsWith(value)), " ending with '" + value + "': ");
        }
    }
}
