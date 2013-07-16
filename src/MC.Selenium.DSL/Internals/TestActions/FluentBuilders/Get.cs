using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal static class Get
    {
        public static readonly Func<IWebElement, Boolean> IsChecked = new Func<IWebElement, Boolean>(AssertionExtensions.IsChecked);
        public static readonly Func<IWebElement, Boolean> IsNotChecked = new Func<IWebElement, Boolean>(AssertionExtensions.IsNotChecked);
        public static readonly Func<IWebElement, Boolean> IsSelected = new Func<IWebElement, Boolean>(AssertionExtensions.IsSelected);
        public static readonly Func<IWebElement, Boolean> IsNotSelected = new Func<IWebElement, Boolean>(AssertionExtensions.IsNotSelected);

        public static readonly Func<IWebElement, Boolean> TagIsTextArea = TagNameIs("textarea");
        public static readonly Func<IWebElement, Boolean> TagIsTextInput = new Func<IWebElement, Boolean>(AssertionExtensions.IsTextInput);
        public static readonly Func<IWebElement, Boolean> TagIsRadio = new Func<IWebElement, Boolean>(AssertionExtensions.IsRadio);
        public static readonly Func<IWebElement, Boolean> TagIsCheckBox = new Func<IWebElement, Boolean>(AssertionExtensions.IsCheckbox);
        public static readonly Func<IWebElement, Boolean> AssertTagIsSelectInput = TagNameIs("select");
        public static readonly Func<IWebElement, Boolean> TagIsOption = TagNameIs("option");

        internal static Func<IWebElement, Boolean> TagNameIs(string value)
        {
            return new Func<IWebElement, Boolean>(_ => _.TagIs(value));
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
            return new Action<IWebElement>(_ => _.Contains(q));
        }

        public static Action<IWebElement> SendKeys(String value)
        {
            return new Action<IWebElement>(_ => _.SendKeys(value));
        }

        private static void DoCheck(this IWebElement element)
        {
            element.IsCheckboxOrRadio();

            if (!element.IsChecked())
            {
                element.Click();
            }
        }

        private static void DoSelect(this IWebElement element)
        {
            element.IsOptionTag();

            if (!element.IsSelected())
            {
                element.Click();
            }
        }

        private static void DoUnCheck(this IWebElement element)
        {
            element.IsCheckbox();

            if (element.IsChecked())
            {
                element.Click();
            }
        }

        private static void DoUnSelect(this IWebElement element)
        {
            element.IsOptionTag();

            if (element.IsSelected())
            {
                element.Click();
            }
        }
    }
}