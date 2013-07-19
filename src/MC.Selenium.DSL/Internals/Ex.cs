using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;

namespace MC.Selenium.DSL
{
    public static class ObservableExtensions
    {
        // todo?? try log ITargetLocator

        public static Boolean TryLog(this IWebDriver driver, TestEventType type, String message)
        {
            var x = driver as ObservableWebDriver;

            if (x == null)
            {
                return false;
            }

            x.Logger.Log(type, message);
            return true;
        }

        public static Boolean TryLog(this IWebElement driver, TestEventType type, String message)
        {
            var x = driver as ObservableWebElement;

            if (x == null)
            {
                return false;
            }

            x.Logger.Log(type, message);
            return true;
        }

    }
}
