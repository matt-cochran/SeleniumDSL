using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Selenium.DSL
{
    public enum LogType
    {
        Info,
        Warning,
        Failure
    }
    public interface ITestLogger
    {
        void Log(String message, LogType type);
    }
}
