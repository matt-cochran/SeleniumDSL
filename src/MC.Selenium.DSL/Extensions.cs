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
        internal static TestAction<T> Then<T>(this TestAction<T> first, TestAction<T> second)
        {
            var result = new TestAction<T>
                {
                    Action = new Action<T>(_ => { first.Action(_); second.Action(_); }),
                    ActionName = String.Format("{0} then {1}", first.ActionName, second.ActionName)
                };
            return result;
        }

        public static bool IsChecked(this IWebElement we)
        {
            var att = we.GetAttribute("checked");

            if (String.IsNullOrWhiteSpace(att))
            {
                return false;
            }

            if (att.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (att.Equals("checked", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public static bool IsSelected(this IWebElement we)
        {
            var att = we.GetAttribute("selected");

            if (String.IsNullOrWhiteSpace(att))
            {
                return false;
            }

            if (att.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (att.Equals("selected", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
