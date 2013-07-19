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
    internal static class PredicateExtensions
    {

        //public static bool IsChecked(this IWebElement we)
        //{
        //    var att = we.GetAttribute("checked");

        //    if (String.IsNullOrWhiteSpace(att))
        //    {
        //        return false;
        //    }

        //    if (att.Equals("true", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return true;
        //    }

        //    if (att.Equals("checked", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        //public static bool IsSelected(this IWebElement we)
        //{
        //    var att = we.GetAttribute("selected");

        //    if (String.IsNullOrWhiteSpace(att))
        //    {
        //        return false;
        //    }

        //    if (att.Equals("true", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return true;
        //    }

        //    if (att.Equals("selected", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        public static Boolean IsChecked(this IWebElement element)
        {
            LogStart(element, "checking if is checked");
            LogTag(element);

            if (!element.TagIs("input"))
            {
                LogEnd(element, "checking if is checked");
                return false;
            }

            string attribute = "checked";
            var att = element.GetAttribute(attribute);
            LogAttributeValue(element, attribute);

            if (String.IsNullOrWhiteSpace(att))
            {
                LogEnd(element, "checking if is checked: false");
                return false; // todo: throw?
            }

            var result =
                att.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                att.Equals("checked", StringComparison.OrdinalIgnoreCase);

            LogEnd(element, "checking if is checked:" + result);

            return result;
        }

        private static void LogEnd(IWebElement element, string message)
        {
            element.TryLog(TestEventType.EndOperation, message);
        }

        private static void LogAttributeValue(IWebElement element, string attribute)
        {
            element.TryLog(TestEventType.Detail, String.Format("attribute '{0}' has value '{1}'", attribute, (element.GetAttribute(attribute) ?? String.Empty)));
        }

        private static void LogTag(IWebElement element)
        {
            element.TryLog(TestEventType.Detail, String.Format("looking at tag <{0}>", element.TagName));
            // TODO: get attributes
        }

        public static Boolean IsCheckboxOrRadio(this IWebElement element)
        {
            LogStart(element, "checking for checkbox or radio");
            LogTag(element);

            if (!element.TagIs("input"))
            {
                return false;
            }

            const string attribute = "type";
            var att = element.GetAttribute(attribute);
            LogAttributeValue(element, "type");

            if (String.IsNullOrWhiteSpace(att))
            {
                return false;
            }

            var result =
                att.Equals("checkbox", StringComparison.OrdinalIgnoreCase) ||
                att.Equals("radio", StringComparison.OrdinalIgnoreCase);

            return result;
        }

        private static void LogStart(IWebElement element, string message)
        {
            element.TryLog(TestEventType.BeginOperation, message);
        }

        private static void LogInfo(IWebElement element, string message)
        {
            element.TryLog(TestEventType.Detail, message);
        }

        public static Boolean IsCheckbox(this IWebElement element)
        {
            LogInfo(element, "checking for checkbox");
            LogTag(element);

            if (!element.TagIs("input"))
            {
                return false;
            }


            const string attribute = "type";
            var att = element.GetAttribute(attribute);
            LogAttributeValue(element, attribute);

            if (String.IsNullOrWhiteSpace(att))
            {
                return false;
            }

            var result = att.Equals("checkbox", StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public static Boolean IsRadio(this IWebElement element)
        {
            LogInfo(element, "checking for radio");
            LogTag(element);

            if (!element.TagIs("input"))
            {
                return false;
            }


            string attribute = "type";
            var att = element.GetAttribute(attribute);
            LogAttributeValue(element, attribute);

            var result = att.Equals("radio", StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public static Boolean IsTextInput(this IWebElement element)
        {
            // TODO: logging
            if (!element.TagIs("input"))
            {
                return false;
            }

            var att = element.GetAttribute("type");
            var result = att.Equals("text", StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public static Boolean IsTextArea(this IWebElement element)
        {
            // TODO: logging
            var result = element.TagIs("textarea");
            return result;
        }

        public static Boolean TagIs(this IWebElement element, String tag)
        {
            // TODO: logging
            var result = element.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public static Boolean IsOptionTag(this IWebElement element)
        {
            // TODO: logging
            var result = element.TagIs("option");
            return result;
        }

        public static Boolean IsNotSelected(this IWebElement element)
        {
            return !IsSelected(element);
        }

        public static Boolean IsNotChecked(this IWebElement element)
        {
            return !IsChecked(element);
        }

        public static Boolean IsSelected(this IWebElement element)
        {
            if (!IsOptionTag(element))
            {
                throw new InvalidOperationException("is not option tag");
            }

            var att = element.GetAttribute("selected");

            if (String.IsNullOrWhiteSpace(att))
            {
                return false;
            }

            var result =
                att.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                att.Equals("selected", StringComparison.OrdinalIgnoreCase);

            return result;
        }

        public static Boolean Contains(this IWebElement element, String value)
        {

            if (String.IsNullOrWhiteSpace(element.Text))
            {
                return false;
            }

            var result = element.Text.Contains(value);
            return result;

        }

    }
}