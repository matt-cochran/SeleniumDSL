using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Selenium.DSL
{
    internal static class Extensions
    {
        internal static TestAction<T> Assert<T>(this TestFunc<T, Boolean> f)
        {
            string name = "asserting that " + f.FunctionName;

            return new TestAction<T>
            {
                ActionName = name,
                Action = _ =>
                    {
                        if(! f.Function(_))
                        {
                            throw new InvalidOperationException(name + " failed.");
                        }
                    }
            };
            
        }

        internal static TestAction<T> Then<T>(this TestAction<T> first, TestAction<T> second)
        {
            var result = new TestAction<T>
                {
                    Action = new Action<T>(_ => { first.Action(_); second.Action(_); }),
                    ActionName = String.Format("{0} then {1}", first.ActionName, second.ActionName)
                };
            return result;
        }


    }
}
