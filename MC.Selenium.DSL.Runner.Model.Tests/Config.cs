using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MC.Selenium.DSL.Runner.Model.Tests
{
    public static class Config
    {
        public static string PhantomJsInstallPath { get { return ConfigurationManager.AppSettings["PhantomJsInstallPath"]; } }
    }
}
