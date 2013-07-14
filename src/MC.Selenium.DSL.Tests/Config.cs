using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using FluentAssertions;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Collections;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.PhantomJS;
using System.Configuration;

namespace MC.Selenium.DSL.Tests
{
    public static class Config
    {
        public static string PhantomJsInstallPath { get { return ConfigurationManager.AppSettings["PhantomJsInstallPath"]; } }
    }
}
