using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;

namespace MC.Selenium.DSL
{
    public class ObservableWebDriver : IWebDriver
    {
        private readonly IWebDriver _Core;
        private readonly ITestEventObserver _Observer;

        /// <summary>
        /// Initializes a new instance of the WebDriverTestContext class.
        /// </summary>
        /// <param name="_Core"></param>
        /// <param name="_Observer"></param>
        public ObservableWebDriver(IWebDriver _Core, ITestEventObserver _Observer)
        {
            this._Core = _Core;
            this._Observer = _Observer;
        }


        public ITestEventObserver Logger
        {
            get
            {
                return _Observer;
            }
        }
        public void Close()
        {
            _Core.Close();
        }

        public string CurrentWindowHandle
        {
            get { return _Core.CurrentWindowHandle; }
        }

        public IOptions Manage()
        {
            return _Core.Manage();
        }

        public INavigation Navigate()
        {
            return _Core.Navigate();
        }

        public string PageSource
        {
            get { return _Core.PageSource; }
        }

        public void Quit()
        {
            _Core.Quit();
        }

        public ITargetLocator SwitchTo()
        {
            throw new NotSupportedException();
            return _Core.SwitchTo(); // TODO: do this
        }

        public string Title
        {
            get { return _Core.Title; }
        }

        public string Url
        {
            get
            {
                return _Core.Url;
            }
            set
            {
                _Core.Url = value;
            }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<string> WindowHandles
        {
            get { return _Core.WindowHandles; }
        }

        public IWebElement FindElement(By by)
        {
            var element = _Core.FindElement(by);

            if (element == null)
            {
                return null;
            }

            var result = new ObservableWebElement(element, _Observer);
            return result;
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

        public void Dispose()
        {
            _Core.Dispose();
        }
    }
}
