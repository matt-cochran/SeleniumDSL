using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Selenium.DSL.Runner.Model
{
    internal static class Extentions
    {
        public static string PopulateVariables(this string template, IDictionary<String, String> variables)
        {
            StringBuilder bld = new StringBuilder(template);

            foreach (var kvp in variables)
            {
                bld.Replace(kvp.Key, kvp.Value);
            }

            return bld.ToString();

        }
    }
}
