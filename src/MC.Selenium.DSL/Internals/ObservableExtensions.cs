using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;

namespace MC.Selenium.DSL
{
    public static class ObservableExtensions
    {
        // todo?? try log ITargetLocator

        public static Boolean TryLog(this IWebDriver driver, TestEventType type, String message)
        {
            var x = driver as ObservableWebDriver;

            if (x == null)
            {
                return false;
            }

            x.Logger.Log(type, message);
            return true;
        }

        public static String GetTag(this IWebElement element)
        {

            if (String.IsNullOrWhiteSpace(element.TagName))
            {
                return String.Empty;
            }

            StringBuilder bld = new StringBuilder();

            bld.Append("<").Append(element.TagName);

            TryAppendAttribute(element, bld, "class");
            TryAppendAttribute(element, bld, "id");
            TryAppendAttribute(element, bld, "name");
            //TryAppendAttribute(element, bld, "style"); // to noisy
            //TryAppendAttribute(element, bld, "title"); // not there often

            if (element.TagName.Equals("input", StringComparison.OrdinalIgnoreCase))
            {
                TryAppendAttribute(element, bld, "type");
            }

            bld.Append(">");

            return bld.ToString();

        }

        private static void TryAppendAttribute(IWebElement element, StringBuilder bld, string name)
        {
            var val = element.GetAttribute(name);

            if (String.IsNullOrWhiteSpace(val))
            {
                return;
            }

            bld.Append(" ").Append(name).Append("='").Append(val).Append("'");
        }

        public static Boolean TryLog(this IWebElement driver, TestEventType type, String message)
        {
            var x = driver as ObservableWebElement;

            if (x == null)
            {
                return false;
            }

            x.Logger.Log(type, message);
            return true;
        }

    }
}
