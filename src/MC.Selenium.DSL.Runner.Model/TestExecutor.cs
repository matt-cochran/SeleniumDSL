using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Selenium.DSL.Runner.Model
{
    internal class TestExecutor : ITestExecutor
    {
        private readonly IWebDriverFactory _DriverFactory;

        /// <summary>
        /// Initializes a new instance of the TestExecutor class.
        /// </summary>
        /// <param name="_DriverFactory"></param>
        public TestExecutor(IWebDriverFactory _DriverFactory)
        {
            this._DriverFactory = _DriverFactory;
        }

        public void Execute(Test test)
        {
            foreach (var browser in test.Browsers)
            {
                var command = test.Command.PopulateVariables(test.Variables);

                using (var driver = _DriverFactory.Build(browser))
                {

                    driver.ExecuteCommand(command);

                    try
                    {
                        driver.Quit();
                    }
                    catch (Exception)
                    {
                        // Ignore errors if unable to close the browser
                    }
                }
                //Assert.AreEqual("", verificationErrors.ToString());
            }
        }

    }
}
