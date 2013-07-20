using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal static class Get
    {
        public static readonly Func<IWebElement, Boolean> IsChecked = new Func<IWebElement, Boolean>(PredicateExtensions.IsChecked);
        public static readonly Func<IWebElement, Boolean> IsNotChecked = new Func<IWebElement, Boolean>(PredicateExtensions.IsNotChecked);
        public static readonly Func<IWebElement, Boolean> IsSelected = new Func<IWebElement, Boolean>(PredicateExtensions.IsSelected);
        public static readonly Func<IWebElement, Boolean> IsNotSelected = new Func<IWebElement, Boolean>(PredicateExtensions.IsNotSelected);

        public static readonly Func<IWebElement, Boolean> TagIsTextArea = TagNameIs("textarea");
        public static readonly Func<IWebElement, Boolean> TagIsTextInput = new Func<IWebElement, Boolean>(PredicateExtensions.IsTextInput);
        public static readonly Func<IWebElement, Boolean> TagIsRadio = new Func<IWebElement, Boolean>(PredicateExtensions.IsRadio);
        public static readonly Func<IWebElement, Boolean> TagIsCheckBox = new Func<IWebElement, Boolean>(PredicateExtensions.IsCheckbox);
        public static readonly Func<IWebElement, Boolean> AssertTagIsSelectInput = TagNameIs("select");
        public static readonly Func<IWebElement, Boolean> TagIsOption = TagNameIs("option");

        public static readonly Action<IWebElement> Check = new Action<IWebElement>(DoCheck);
        public static readonly Action<IWebElement> UnCheck = new Action<IWebElement>(DoUnCheck);
        public static readonly Action<IWebElement> Select = new Action<IWebElement>(DoSelect);
        public static readonly Action<IWebElement> UnSelect = new Action<IWebElement>(DoUnSelect);
        public static readonly Action<IWebElement> Clear = new Action<IWebElement>(DoClear);
        public static readonly Action<IWebElement> Click = new Action<IWebElement>(DoClick);
        public static readonly Action<IWebElement> Nothing = new Action<IWebElement>(DoNothing);

        internal static Func<IWebElement, Boolean> TagNameIs(string value)
        {
            return new Func<IWebElement, Boolean>(_ =>
                {
                    _.TryLog(TestEventType.BeginOperation, "check tag name is <" + value + "> for " + _.GetTag() + ".");
                   var result = _.TagIs(value);
                   _.TryLog(TestEventType.EndOperation, "check tag name is <" + value + ">. Result is " + result + " becase tag is " + _.GetTag() + ".");
                   return result;
                });
        }

        private static void DoClick(IWebElement element)
        {
            element.TryLog(TestEventType.BeginOperation, "clicking " + element.GetTag() + ".");
            element.Click();
            element.TryLog(TestEventType.EndOperation, "clicking " + element.GetTag() + ".");
        }

        private static void DoClear(IWebElement element)
        {
            element.TryLog(TestEventType.BeginOperation, "clearing " + element.GetTag() + ".");
            element.Clear();
            element.TryLog(TestEventType.EndOperation, "clearing <" + element.GetTag() + ">.");
        }

        private static void DoNothing(IWebElement obj)
        {
            return;
        }

        internal static Func<IWebElement, Boolean> GetContains(string q)
        {
            return new Func<IWebElement, Boolean>(_ => _.Contains(q));
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