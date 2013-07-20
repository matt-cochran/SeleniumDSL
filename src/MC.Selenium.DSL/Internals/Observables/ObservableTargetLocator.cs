using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;

namespace MC.Selenium.DSL
{
    public class ObservableTargetLocator : ITargetLocator
    {
        private readonly ITargetLocator _Core;
        private readonly ITestEventObserver _Observer;

        /// <summary>
        /// Initializes a new instance of the ObservableTargetLocator class.
        /// </summary>
        /// <param name="_Core"></param>
        /// <param name="_Observer"></param>
        public ObservableTargetLocator(ITargetLocator _Core, ITestEventObserver _Observer)
        {
            this._Core = _Core;
            this._Observer = _Observer;
        }

        public ITestEventObserver Logger { get { return _Observer; } }

        public IWebElement ActiveElement()
        {
            var core = _Core.ActiveElement();
            return GetElement(core);
        }

        private IWebElement GetElement(IWebElement core)
        {
            if (core == null)
            {
                return null;
            }

            var result = new ObservableWebElement(core, _Observer);
            return result;
        }

        public IAlert Alert()
        {
            return _Core.Alert();
        }

        public IWebDriver DefaultContent()
        {
            var core = _Core.DefaultContent();
            return GetWebDriver(core);
        }

        public IWebDriver Frame(IWebElement frameElement)
        {
            var core = _Core.Frame(frameElement);
            return GetWebDriver(core);
        }

        private IWebDriver GetWebDriver(IWebDriver core)
        {
            if (core == null)
            {
                return null;
            }

            var result = new ObservableWebDriver(core, _Observer);
            return result;
        }

        public IWebDriver Frame(string frameName)
        {
            var core = _Core.Frame(frameName);
            return GetWebDriver(core);
        }

        public IWebDriver Frame(int frameIndex)
        {
            var core = _Core.Frame(frameIndex);
            return GetWebDriver(core);
        }

        public IWebDriver Window(string windowName)
        {
            var core = _Core.Window(windowName);
            return GetWebDriver(core);
        }
    }
}
