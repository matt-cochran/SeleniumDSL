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
    public static class Extensions
    {
        public static Action<T> Then<T>(this Action<T> first, Action<T> second)
        {
            var result = new Action<T>(_ => { first(_); second(_); });
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
