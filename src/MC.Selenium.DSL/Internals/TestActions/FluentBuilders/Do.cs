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
    internal static class Do
    {
        public static readonly Action<IWebElement> AssertIsChecked = new Action<IWebElement>(AssertionExtensions.AssertIsChecked);
        public static readonly Action<IWebElement> AssertNotChecked = new Action<IWebElement>(AssertionExtensions.AssertIsNotChecked);
        public static readonly Action<IWebElement> AssertIsSelected = new Action<IWebElement>(AssertionExtensions.AssertIsSelected);
        public static readonly Action<IWebElement> AssertNotSelected = new Action<IWebElement>(AssertionExtensions.AssertIsNotSelected);

        public static readonly Action<IWebElement> AssertTagIsTextArea = AssertTagName("textarea");
        public static readonly Action<IWebElement> AssertTagIsTextInput = new Action<IWebElement>(AssertionExtensions.AssertIsTextInput);
        public static readonly Action<IWebElement> AssertTagIsRadioInput = new Action<IWebElement>(AssertionExtensions.AssertIsRadio);
        public static readonly Action<IWebElement> AssertTagIsCheckBoxInput = new Action<IWebElement>(AssertionExtensions.AssertIsCheckbox);
        public static readonly Action<IWebElement> AssertTagIsSelectInput = AssertTagName("select");
        public static readonly Action<IWebElement> AssertTagIsOption = AssertTagName("option");
        
        internal static Action<IWebElement> AssertTagName(string value)
        {
            return new Action<IWebElement>(_ => _.AssertTagIs(value, String.Format("{0} tag required but received {1}", value, _.TagName)));
        }

        public static readonly Action<IWebElement> Check = new Action<IWebElement>(DoCheck);
        public static readonly Action<IWebElement> UnCheck = new Action<IWebElement>(DoUnCheck);

        public static readonly Action<IWebElement> Select = new Action<IWebElement>(DoSelect);
        public static readonly Action<IWebElement> UnSelect = new Action<IWebElement>(DoUnSelect);

        public static readonly Action<IWebElement> Clear = new Action<IWebElement>(_ => _.Clear());
        public static readonly Action<IWebElement> Click = new Action<IWebElement>(_ => _.Click());

        public static readonly Action<IWebElement> Nothing = new Action<IWebElement>(_ => {});

        internal static Action<IWebElement> AssertContains(string q)
        {
            return new Action<IWebElement>(_ => _.AssertContains(q));
        }


        public static Action<IWebElement> SendKeys(String value)
        {
            return new Action<IWebElement>(_ => _.SendKeys(value));
        }


        //private static void SendKeys(this IWebElement element, String value)
        //{
        //    element.Clear();
        //    element.SendKeys(value);
        //}



        private static void DoCheck(this IWebElement element)
        {
            element.AssertIsCheckboxOrRadio();

            if (!element.IsChecked())
            {
                element.Click();
            }
        }

        private static void DoSelect(this IWebElement element)
        {
            element.AssertIsOptionTag();

            if (!element.IsSelected())
            {
                element.Click();
            }
        }

        private static void DoUnCheck(this IWebElement element)
        {
            element.AssertIsCheckbox();

            if (element.IsChecked())
            {
                element.Click();
            }
        }

        private static void DoUnSelect(this IWebElement element)
        {
            element.AssertIsOptionTag();

            if (element.IsSelected())
            {
                element.Click();
            }
        }



        
    }
}