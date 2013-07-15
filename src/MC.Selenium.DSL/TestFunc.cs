using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class TestFunc<T, U>
    {
        public Func<T, U> Function { get; set; }
        public String FunctionName { get; set; }
    }

    internal static class TestFunc
    {
        public static TestFunc<T, U> Create<T, U>(Func<T, U> func, String name)
        {
            return new TestFunc<T, U>
            {
                Function = func,
                FunctionName = name
            };
        }
    }
}
