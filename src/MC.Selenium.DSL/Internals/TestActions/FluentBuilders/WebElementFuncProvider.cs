using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class WebElementFuncProvider
    {
        private WebElementDriver _op = new WebElementDriver();

        private static Func<T, U> GetFunc<T, U>(Func<T, U> f)
        {
            return f;
        }

        public TestFunc<IWebElement, Boolean> IsOption()
        {
            var f = GetFunc<IWebElement, Boolean>(_op.GetIsOptionTag);
            return TestFunc.Create(f, "is option");
        }
        internal TestFunc<IWebElement, Boolean> IsTextArea()
        {
            var f = GetFunc<IWebElement, Boolean>(_op.GetIsTextArea);
            return TestFunc.Create(f, "is text area");
        }

        internal TestFunc<IWebElement, Boolean> IsTextBox()
        {
            var f = GetFunc<IWebElement, Boolean>(_op.GetIsTextInput);
            return TestFunc.Create(f, "is text box");
        }
        public TestFunc<IWebElement, Boolean> IsRadioButton()
        {
            var f = GetFunc<IWebElement, Boolean>(_op.GetIsRadio);
            return TestFunc.Create(f, "is radio");
        }

        internal TestFunc<IWebElement, Boolean> IsCheckBox()
        {
            var f = GetFunc<IWebElement, Boolean>(_op.GetIsCheckbox);
            return TestFunc.Create(f, "is check box");
        }

        public TestFunc<IWebElement, Boolean> IsChecked()
        {
            var f = GetFunc<IWebElement, Boolean>(_op.GetIsChecked);
            return TestFunc.Create(f, "is checked");
        }

        public TestFunc<IWebElement, Boolean> IsSelected()
        {
            var f = GetFunc<IWebElement, Boolean>(_op.GetIsSelected);
            return TestFunc.Create(f, "is selected");
        }

        public TestFunc<IWebElement, Boolean> IsNotChecked()
        {
            var f = GetFunc<IWebElement, Boolean>(_op.GetIsNotChecked);
            return TestFunc.Create(f, "is not checked");
        }

        public TestFunc<IWebElement, Boolean> IsNotSelected()
        {
            var f = GetFunc<IWebElement, Boolean>(_op.GetIsNotSelected);
            return TestFunc.Create(f, "is not selected");
        }
    }
}
