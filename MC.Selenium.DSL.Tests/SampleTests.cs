using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using MC.Selenium.DSL;

namespace SeleniumTests
{
    [TestFixture]
    public class SampleTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://www.google.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void LineByLineTest()
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); // TODO: 
            driver.ExecuteCommand(@"go to www.google.com");
            //driver.ExecuteCommand(@"clear element with id 'gbqfq'");
            driver.ExecuteCommand(@"clear element named 'q'");
            driver.ExecuteCommand(@"send 'asdf' to element named 'q'");

            //driver.FindElement(By.Id("10")).GetAttribute("x").EndsWith("x");
            // "assert element with id "10" has attribute "x" that ends with "x"

            //driver.FindElement(By.Id("x")).GetCssValue("asdf").Contains("asdf");
            //driver.FindElement(By.Id("x")).Selected;

            //    driver.FindElement(By.Id("x")).Text.Equals("asdf")
        }

        [Test]
        public void MultiLineByLineTest()
        {
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10)); // TODO: 
            driver.ExecuteCommand(@"go to www.google.com, clear element named 'q' then send 'asdf' to element named 'q'");
        }


        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
