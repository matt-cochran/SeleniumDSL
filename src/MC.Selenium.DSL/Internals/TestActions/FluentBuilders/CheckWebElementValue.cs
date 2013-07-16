using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class WebElementPredicate
    {
        public TestFunc<IWebElement, Boolean> IsOption()
        {
            return TestFunc.Create(Get.TagIsOption, "is option");
        }
        internal TestFunc<IWebElement, Boolean> IsTextArea()
        {
            return TestFunc.Create(Get.TagIsTextArea, "is text area");
        }

        internal TestFunc<IWebElement, Boolean> IsTextBox()
        {
            return TestFunc.Create(Get.TagIsTextInput, "is text box");
        }
        public TestFunc<IWebElement, Boolean> IsRadioButton()
        {
            return TestFunc.Create(Get.TagIsRadio, "is radio");
        }

        internal TestFunc<IWebElement, Boolean> IsCheckBox()
        {
            return TestFunc.Create(Get.TagIsCheckBox, "is check box");
        }

        public TestFunc<IWebElement, Boolean> IsChecked() { return _IsChecked; }
        public TestFunc<IWebElement, Boolean> IsSelected() { return _IsSelected; }
        public TestFunc<IWebElement, Boolean> IsNotChecked() { return _NotChecked; }
        public TestFunc<IWebElement, Boolean> IsNotSelected() { return _NotSelected; }

        private static readonly TestFunc<IWebElement, Boolean> _IsSelected = TestFunc.Create(Get.IsSelected, "is selected");
        private static readonly TestFunc<IWebElement, Boolean> _NotSelected = TestFunc.Create(Get.IsNotSelected, "is not selected");
        private static readonly TestFunc<IWebElement, Boolean> _NotChecked = TestFunc.Create(Get.IsNotChecked, "not checked");
        private static readonly TestFunc<IWebElement, Boolean> _IsChecked = TestFunc.Create(Get.IsChecked, "is checked");
    }
}
