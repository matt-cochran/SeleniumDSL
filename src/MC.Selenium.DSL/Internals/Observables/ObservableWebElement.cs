using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;

namespace MC.Selenium.DSL
{

    public class ObservableWebElement : IWebElement
    {
        private  readonly IWebElement _Core;
        private  readonly ITestEventObserver _Observer;

        public ITestEventObserver Logger
        {
            get
            {
                return _Observer;
            }
        }
        /// <summary>
        /// Initializes a new instance of the ElementTestContext class.
        /// </summary>
        /// <param name="_Core"></param>
        /// <param name="_Observer"></param>
        public ObservableWebElement(IWebElement _Core, ITestEventObserver _Observer)
        {
            this._Core = _Core;
            this._Observer = _Observer;
        }

        public void Clear()
        {
            _Core.Clear();
        }

        public void Click()
        {
            _Core.Click();
        }

        public bool Displayed
        {
            get { return _Core.Displayed; }
        }

        public bool Enabled
        {
            get { return _Core.Enabled; }
        }

        public string GetAttribute(string attributeName)
        {
            return _Core.GetAttribute(attributeName);
        }

        public string GetCssValue(string propertyName)
        {
            return _Core.GetCssValue(propertyName);
        }

        public Point Location
        {
            get { return _Core.Location; }
        }

        public bool Selected
        {
            get { return _Core.Selected; }
        }

        public void SendKeys(string text)
        {
            _Core.SendKeys(text);
        }

        public Size Size
        {
            get { return _Core.Size; }
        }

        public void Submit()
        {
            _Core.Submit();
        }

        public string TagName
        {
            get { return _Core.TagName; }
        }

        public string Text
        {
            get { return _Core.Text; }
        }

        public IWebElement FindElement(By by)
        {
            var result = _Core.FindElement(by);
            if (result == null)
            {
                return null;
            }
            return new ObservableWebElement(result, _Observer);
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            var elements = _Core.FindElements(by);

            if (elements == null)
            {
                return null;
            }

            if (elements.Count == 0)
            {
                return elements;
            };

            var result = new ReadOnlyCollection<IWebElement>(elements.Select(_ => new ObservableWebElement(_, _Observer)).ToArray());

            return result;
        }
    }
}
