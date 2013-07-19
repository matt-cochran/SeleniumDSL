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
            if (!element.TagIs("input"))
            {
                return false; // TODO: throw?
            }


            var att = element.GetAttribute("checked");

            

            if (String.IsNullOrWhiteSpace(att))
            {
                return false; // todo: throw?
            }

            return
                att.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                att.Equals("checked", StringComparison.OrdinalIgnoreCase);
        }

        public static Boolean IsCheckboxOrRadio(this IWebElement element)
        {
            if (!element.TagIs("input"))
            {
                return false;
            }


            var att = element.GetAttribute("type");

            if (String.IsNullOrWhiteSpace(att))
            {
                return false;
            }

            var result =
                att.Equals("checkbox", StringComparison.OrdinalIgnoreCase) ||
                att.Equals("radio", StringComparison.OrdinalIgnoreCase);

            return result;
        }

        public static Boolean IsCheckbox(this IWebElement element)
        {
            if (!element.TagIs("input"))
            {
                return false;
            }


            var att = element.GetAttribute("type");

            if (String.IsNullOrWhiteSpace(att))
            {
                return false;
            }

            var result = att.Equals("checkbox", StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public static Boolean IsRadio(this IWebElement element)
        {
            if (!element.TagIs("input"))
            {
                return false;
            }


            var att = element.GetAttribute("type");

            var result = att.Equals("radio", StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public static Boolean IsTextInput(this IWebElement element)
        {
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
            var result = element.TagIs("textarea");
            return result;
        }

        public static Boolean TagIs(this IWebElement element, String tag)
        {
            var result = element.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase);
            return result;
        }

        public static Boolean IsOptionTag(this IWebElement element)
        {
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