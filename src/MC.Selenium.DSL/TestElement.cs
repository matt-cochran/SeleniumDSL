using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class TestElement
    {
        public By Finder { get; set; }
        public String Name { get; set; }

        internal static TestElement Create(By finder, string name)
        {
            return new TestElement
            {
                 Finder = finder,
                 Name = name
            };
        }
    }
}
