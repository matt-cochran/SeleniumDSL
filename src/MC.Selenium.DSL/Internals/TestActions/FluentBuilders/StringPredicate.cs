using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class StringPredicate
    {
        public TestFunc<String, Boolean> Contains(string value)
        {
            return new TestFunc<string, bool>()
            {
                FunctionName = String.Format(" contains '{0}'", value),
                Function = _ => _.Contains(value)
            };
        }

        public TestFunc<String, Boolean> EndsWith(string value)
        {
            return new TestFunc<string, bool>()
            {
                FunctionName = String.Format(" ends with '{0}'", value),
                Function = _ => _.EndsWith(value)
            };
        }

        internal TestFunc<String, Boolean> BeginsWith(string value)
        {
            return new TestFunc<string, bool>()
            {
                FunctionName = String.Format(" begins with '{0}'", value),
                Function = _ => _.StartsWith(value)
            };
        }

        internal TestFunc<String, Boolean> IsEqualTo(string value)
        {
            return new TestFunc<string, bool>()
            {
                FunctionName = String.Format(" equals '{0}'", value),
                Function = _ => _.Equals(value)
            };
        }

        internal TestFunc<String, Boolean> Nothing()
        {
            return new TestFunc<string, bool>()
            {
                FunctionName = String.Empty,
                Function = _ => true
            };
        }
    }
}
