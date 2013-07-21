using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    public interface IWebElementDriver
    {
        void Unselect(IWebElement element);
        bool GetTagNameEquals(IWebElement element, string value);
        void Uncheck(IWebElement element);
        void Check(IWebElement element);
        void Select(IWebElement element);
        void SendKeys(IWebElement element, string value);
        bool Contains(IWebElement element, string value);
        void Clear(IWebElement element);
        void Click(IWebElement element);
        bool GetIsChecked(IWebElement element);
        Boolean GetIsCheckboxOrRadio(IWebElement element);
        bool GetIsCheckbox(IWebElement element);
        bool GetIsRadio(IWebElement element);
        bool GetIsTextInput(IWebElement element);
        bool GetIsTextArea(IWebElement element);
        bool GetTagIs(IWebElement element, string tagName);
        bool GetIsOptionTag(IWebElement element);
        bool GetIsNotSelected(IWebElement element);
        bool GetIsNotChecked(IWebElement element);
        bool GetIsSelected(IWebElement element);
        void DoNothing(IWebElement element);
    }
    public class WebElementDriver : IWebElementDriver
    {
        public void Unselect(IWebElement element)
        {
            element.IsOptionTag();

            if (element.IsSelected())
            {
                element.Click();
            }
        }

        public bool GetTagNameEquals(IWebElement _, string value)
        {
            var result = _.TagName.Equals(value);
            return result;

            //_.TryLog(TestEventType.BeginOperation, "check tag name is <" + value + "> for " + _.GetTag() + ".");

            //_.TryLog(TestEventType.EndOperation, "check tag name is <" + value + ">. Result is " + result + " becase tag is " + _.GetTag() + ".");

        }

        public void Uncheck(IWebElement element)
        {
            element.IsCheckbox();

            if (element.IsChecked())
            {
                element.Click();
            }
        }

        public void Check(IWebElement element)
        {
            element.IsCheckboxOrRadio();

            if (!element.IsChecked())
            {
                element.Click();
            }
        }

        public void Select(IWebElement element)
        {
            element.IsOptionTag();

            if (!element.IsSelected())
            {
                element.Click();
            }
        }

        public void SendKeys(IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        public bool Contains(IWebElement element, string value)
        {
            if (String.IsNullOrWhiteSpace(element.Text))
            {
                return false;
            }

            var result = element.Text.Contains(value);
            return result;
        }

        public void Clear(IWebElement element)
        {
            element.TryLog(TestEventType.BeginOperation, "clearing " + element.GetTag() + ".");
            element.Clear();
            element.TryLog(TestEventType.EndOperation, "clearing <" + element.GetTag() + ">.");

        }

        public void Click(IWebElement element)
        {
            element.TryLog(TestEventType.BeginOperation, "clicking " + element.GetTag() + ".");
            element.Click();
            element.TryLog(TestEventType.EndOperation, "clicking " + element.GetTag() + ".");
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
            element.TryLog(TestEventType.Detail, String.Format("looking at element <{0}>", element.TagName));
        }


        public bool GetIsChecked(IWebElement element)
        {
            LogOpStart(element, "checking if is checked");
            LogTag(element);

            if (!element.TagIs("input"))
            {
                LogEnd(element, "checking if is checked. result is false because tag is <" + element.TagName + ">");
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

        private static void LogOpStart(IWebElement element, string message)
        {
            element.TryLog(TestEventType.BeginOperation, message);
        }

        private static void LogInfo(IWebElement element, string message)
        {
            element.TryLog(TestEventType.Detail, message);
        }

        public Boolean GetIsCheckboxOrRadio(IWebElement element)
        {
            LogOpStart(element, "checking for checkbox or radio");
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

        public bool GetIsCheckbox(IWebElement element)
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

        public bool GetIsRadio(IWebElement element)
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

        public bool GetIsTextInput(IWebElement element)
        {
            element.TryLog(TestEventType.BeginOperation, "checking element is text input.");

            if (!element.TagIs("input"))
            {
                return false;
                element.TryLog(TestEventType.EndOperation, "checking element is text input. Result is false because element tag <" + element.TagName + "> is not <input> tag.");

            }

            var att = element.GetAttribute("type");
            var result = att.Equals("text", StringComparison.OrdinalIgnoreCase);

            element.TryLog(TestEventType.EndOperation,
                String.Format(
                    "checking element is text input. Result is {0} because element tag is <{1}  type='{2}'>.",
                    result,
                    element.TagName,
                    element.GetAttribute("type")));


            return result;
        }

        public bool GetIsTextArea(IWebElement element)
        {
            element.TryLog(TestEventType.BeginOperation, "checking element is textarea.");
            var result = element.TagIs("textarea");
            element.TryLog(TestEventType.EndOperation, String.Format("checking element is textarea. Result is {0} becase tag is <{1}>.", result, element.TagName));
            return result;
        }

        public bool GetTagIs(IWebElement element, string tagName)
        {
            element.TryLog(TestEventType.BeginOperation, "checking tag name is " + tagName + ".");
            var result = element.TagName.Equals(tagName, StringComparison.OrdinalIgnoreCase);
            element.TryLog(TestEventType.EndOperation, "checking tag name is " + tagName + ". Result is " + result);

            return result;
        }

        public bool GetIsOptionTag(IWebElement element)
        {
            // TODO: logging
            var result = element.TagIs("option");
            return result;
        }

        public bool GetIsNotSelected(IWebElement element)
        {
            return !GetIsSelected(element);
        }

        public bool GetIsNotChecked(IWebElement element)
        {
            return !this.GetIsChecked(element);
        }

        public bool GetIsSelected(IWebElement element)
        {
            if (!this.GetIsOptionTag(element))
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

        public void DoNothing(IWebElement element)
        {
            return;
        }

        public string GetAttribute(IWebElement element, String attributeName)
        {
            return element.GetAttribute(attributeName);
        }
    }
}
