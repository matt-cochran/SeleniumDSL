using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    internal class TestActionAssert
    {
        internal TestAction<IWebElement> IsTextArea()
        {
            return TestAction.Create(Do.AssertTagIsTextArea, "is text area");
        }

        internal TestAction<IWebElement> IsTextBox()
        {
            return TestAction.Create(Do.AssertTagIsTextInput, "is text box");
        }
        public TestAction<IWebElement> IsRadioButton()
        {
            return TestAction.Create(Do.AssertTagIsRadioInput, "is radio");
        }

        internal TestAction<IWebElement> IsCheckBox()
        {
            return TestAction.Create(Do.AssertTagIsCheckBoxInput, "is check box");
        }

        public TestAction<IWebElement> IsChecked() { return _AssertIsChecked; }
        public TestAction<IWebElement> IsSelected() { return _AssertIsSelected; }
        public TestAction<IWebElement> IsNotChecked() { return _AssertNotChecked; }
        public TestAction<IWebElement> IsNotSelected() { return _AssertNotSelected; }

        private static readonly TestAction<IWebElement> _AssertIsSelected = TestAction.Create(Do.AssertIsSelected, "asserting is selected");
        private static readonly TestAction<IWebElement> _AssertNotSelected = TestAction.Create(Do.AssertNotSelected, "asserting is not selected");
        private static readonly TestAction<IWebElement> _AssertNotChecked = TestAction.Create(Do.AssertNotChecked, "asserting not checked");
        private static readonly TestAction<IWebElement> _AssertIsChecked = TestAction.Create(Do.AssertIsChecked, "asserting is checked");

    }
}
