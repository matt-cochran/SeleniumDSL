using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Selenium.DSL
{
    public interface ITestEventObserver
    {
        void Log(TestEventType type, String message);
    }
}
