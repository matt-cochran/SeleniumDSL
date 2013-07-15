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
        private readonly ITestEventObserver _Logger;

        /// <summary>
        /// Initializes a new instance of the TestExecutor class.
        /// </summary>
        /// <param name="_DriverFactory"></param>
        public TestExecutor(IWebDriverFactory driverFactory, ITestEventObserver logger)
        {
            _DriverFactory = driverFactory;
            _Logger = logger;
        }

        public void Execute(Test test)
        {
            foreach (var browser in test.Browsers)
            {
                var command = test.Command.PopulateVariables(test.Variables);



                using (var driver = _DriverFactory.Build(browser))
                {
                    driver.ExecuteCommand(command, _Logger);

                    try
                    {
                        driver.Quit();
                    }
                    catch (Exception ex)
                    {
                        _Logger.Log(TestEventType.Warning, "Unable to close browser: " + ex.Message);
                    }
                }
                //Assert.AreEqual("", verificationErrors.ToString());
            }
        }

    }
}
