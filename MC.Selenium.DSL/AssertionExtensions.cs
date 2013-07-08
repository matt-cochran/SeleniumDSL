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
    internal static class AssertionExtensions
    {
        public static void AssertIsChecked(this IWebElement element)
        {
            element.AssertTagIs(
                "input",
                String.Format("element can not be a checkbox or radio, is not a an input tag: <{0} >", element.TagName));


            var att = element.GetAttribute("checked");

            if (String.IsNullOrWhiteSpace(att))
            {
                throw new InvalidOperationException("'checked' attribute is null or empty");
            }
            
            if (!att.Equals("true", StringComparison.OrdinalIgnoreCase) &&
                !att.Equals("checked", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("'checked' attribute is not 'true' or 'checked'");
            }
        }

        public static void AssertIsCheckboxOrRadio(this IWebElement element)
        {
            element.AssertTagIs(
                "input",
                String.Format("element can not be a checkbox or radio, is not a an input tag: <{0} >", element.TagName));

            
            var att = element.GetAttribute("type");

            if (String.IsNullOrWhiteSpace(att) ||
                !(att.Equals("checkbox", StringComparison.OrdinalIgnoreCase) ||
                   att.Equals("radio", StringComparison.OrdinalIgnoreCase)
                ))
            {
                throw new InvalidOperationException("input tag is not a checkbox or radio: <input type=" + (att ?? String.Empty) + ">");
            }
        }

        public static void AssertIsCheckbox(this IWebElement element)
        {
            element.AssertTagIs(
                "input",
                String.Format("element can not be a checkbox, is not a an input tag: <{0} >", element.TagName));


            var att = element.GetAttribute("type");

            if (String.IsNullOrWhiteSpace(att) || !att.Equals("checkbox", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("input tag is not a checkbox: <input type=" + (att ?? String.Empty) + ">");
            }
        }

        public static void AssertIsRadio(this IWebElement element)
        {
            element.AssertTagIs(
                "input",
                String.Format("element can not be a radio button, is not a an input tag: <{0} >", element.TagName));


            var att = element.GetAttribute("type");

            if (String.IsNullOrWhiteSpace(att) || !att.Equals("radio", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("input tag is not a radio button: <input type=" + (att ?? String.Empty) + ">");
            }
        }

        public static void AssertIsTextInput(this IWebElement element)
        {
            element.AssertTagIs(
                "input",
                String.Format("element can not be a text input, is not a an input tag: <{0} >", element.TagName));


            var att = element.GetAttribute("type");

            if (String.IsNullOrWhiteSpace(att) || !att.Equals("text", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("input tag is not a text input: <input type=" + (att ?? String.Empty) + ">");
            }
        }

        public static void AssertTagIs(this IWebElement element, String tag, String message)
        {
            if (!element.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertIsOptionTag(this IWebElement element)
        {
            element.AssertTagIs(
                "option",
                String.Format("element is not an option tag: <{0} >", element.TagName));
        }

        public static void AssertIsNotSelected(this IWebElement element)
        {
            AssertIsOptionTag(element);

            var att = element.GetAttribute("selected");

            var result = String.IsNullOrWhiteSpace(att) ||
                         att.Equals("false", StringComparison.OrdinalIgnoreCase);

            if (!result)
            {
                throw new InvalidOperationException("'selected' attribute is not 'false' or empty");
            }

        }

        public static void AssertIsNotChecked(this IWebElement element)
        {
            {
                var att = element.GetAttribute("checked");

                var result = String.IsNullOrWhiteSpace(att) ||
                             att.Equals("false", StringComparison.OrdinalIgnoreCase);

                if (!result)
                {
                    throw new InvalidOperationException("'checked' attribute is not 'false' or empty");
                }
            }
        }

        public static void AssertIsSelected(this IWebElement element)
        {
            AssertIsOptionTag(element);

            var att = element.GetAttribute("selected");

            if (String.IsNullOrWhiteSpace(att))
            {
                throw new InvalidOperationException("'selected' attribute is null or empty");
            }

            if (!att.Equals("true", StringComparison.OrdinalIgnoreCase) &&
                !att.Equals("selected", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("'selected' attribute is not 'true' or 'selected'");
            }
        }

        public static void AssertContains(this IWebElement element, String value)
        {
            if (String.IsNullOrWhiteSpace(element.Text))
            {
                throw new InvalidOperationException("element text is empty, it does not contain value " + value);
            }

            if (!element.Text.Contains(value))
            {
                throw new InvalidOperationException(String.Format("element text is {0}, it does not contain value {1}", element.Text, value));
            }

        }

    }
}