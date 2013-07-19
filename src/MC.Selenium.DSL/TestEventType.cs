using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Selenium.DSL
{
    public enum TestEventType
    {
        Message = 0,
        Warning = 1,
        Failure = 3,
        Detail = 4,
        BeginOperation = 5,
        EndOperation = 6
    }
}
