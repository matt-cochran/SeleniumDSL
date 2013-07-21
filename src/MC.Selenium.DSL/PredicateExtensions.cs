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
        private static readonly WebElementDriver _op = new WebElementDriver();


        public static Boolean IsChecked(this IWebElement element)
        {
            return _op.GetIsChecked(element);
        }


        public static Boolean IsCheckboxOrRadio(this IWebElement element)
        {
            return _op.GetIsCheckboxOrRadio(element);
        }



        public static Boolean IsCheckbox(this IWebElement element)
        {
            return _op.GetIsCheckbox(element);
        }

        public static Boolean IsRadio(this IWebElement element)
        {
            return _op.GetIsRadio(element);
        }

        public static Boolean IsTextInput(this IWebElement element)
        {
            return _op.GetIsTextInput(element);
        }

        public static Boolean IsTextArea(this IWebElement element)
        {
            return _op.GetIsTextArea(element);
        }

        public static Boolean TagIs(this IWebElement element, String tag)
        {
            return _op.GetTagIs(element, tag);
        }

        public static Boolean IsOptionTag(this IWebElement element)
        {
            return _op.GetIsOptionTag(element);
        }

        public static Boolean IsNotSelected(this IWebElement element)
        {
            return _op.GetIsNotSelected(element);
        }

        public static Boolean IsNotChecked(this IWebElement element)
        {
            return _op.GetIsNotChecked(element);
        }

        public static Boolean IsSelected(this IWebElement element)
        {
            return _op.GetIsSelected(element);
        }

        public static Boolean Contains(this IWebElement element, String value)
        {

            return _op.Contains(element, value);

        }

    }
}