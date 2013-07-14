using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Selenium.DSL.Runner.Model
{
    internal class Test
    {
        public string Target { get; set; }
        public string Purpose { get; set; }
        public String[] Browsers { get; set; }
        public IDictionary<String, String> Variables { get; set; }
        public string Command { get; set; }
    }
}