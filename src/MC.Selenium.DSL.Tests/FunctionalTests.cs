using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;


namespace MC.Selenium.DSL.Tests
{
    [TestFixture]
    class FunctionalTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new PhantomJSDriver(PhantomJSDriverService.CreateDefaultService(Config.PhantomJsInstallPath), new PhantomJSOptions());
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

        public static String CreateTempFile(String contents)
        {
            string f = Guid.NewGuid().ToString() + ".htm";
            var name = @"c:\temp\" + f;

            using (var w = File.CreateText(name))
            {
                w.Write(contents);
                w.Flush();
            }

            return "file:///c:/temp/" + f;
        }

        [Test]
        public void TextAreaElementTests()
        {
            var html = "<html><body><textarea id='x'>text</textarea></body></html>";

            var url = CreateTempFile(html);

            driver.ExecuteCommand("go to " + url);
            driver.ExecuteCommand("assert element with id 'x' has text with value 'text'");
            driver.ExecuteCommand("clear element with id 'x'");
            driver.ExecuteCommand("assert element with id 'x' has text with value ''");

            var val = Guid.NewGuid().ToString();

            driver.ExecuteCommand("send '" + val + "' to element with id 'x'");
            driver.ExecuteCommand("assert element with id 'x' has text with value '" + val + "'");
        }

        [Test]
        public void TextAreaTests()
        {
            var html = "<html><body><textarea id='x'>text</textarea></body></html>";

            var url = CreateTempFile(html);

            driver.ExecuteCommand("go to " + url);
            driver.ExecuteCommand("assert text area with id 'x' has text with value 'text'");
            //driver.ExecuteCommand("clear text area with id 'x'");
            //driver.ExecuteCommand("assert textarea with id 'x' has text with value ''");

            var val = Guid.NewGuid().ToString();

            driver.ExecuteCommand("send '" + val + "' to text area with id 'x'");
            driver.ExecuteCommand("assert text area with id 'x' has text with value '" + val + "'");
        }

        [Test]
        public void TextInputElementTests()
        {
            var html = "<html><body><input type='text' value='text' id='x'></input></body></html>";

            var url = CreateTempFile(html);

            driver.ExecuteCommand("go to " + url);
            driver.ExecuteCommand("assert element with id 'x' has text with value 'text'");
            driver.ExecuteCommand("clear element with id 'x'");
            driver.ExecuteCommand("assert element with id 'x' has text with value ''");

            var val = Guid.NewGuid().ToString();

            driver.ExecuteCommand("send '" + val + "' to element with id 'x'");
            driver.ExecuteCommand("assert element with id 'x' has text with value '" + val + "'");
        }

        [Test]
        public void TextInputTests()
        {
            var html = "<html><body><input type='text' value='text' id='x'></input></body></html>";

            var url = CreateTempFile(html);

            driver.ExecuteCommand("go to " + url);
            driver.ExecuteCommand("assert text box with id 'x' has text with value 'text'");
            driver.ExecuteCommand("clear text box  with id 'x'");
            driver.ExecuteCommand("assert text box  with id 'x' has text with value ''");

            var val = Guid.NewGuid().ToString();

            driver.ExecuteCommand("send '" + val + "' to text box  with id 'x'");
            driver.ExecuteCommand("assert text box  with id 'x' has text with value '" + val + "'");

        }

        [Test]
        public void CheckBoxTests()
        {
            var html = "<html><body><input type='checkbox' id='x' checked='true' /></body></html>";

            var url = CreateTempFile(html);

            driver.ExecuteCommand("go to " + url);
            driver.ExecuteCommand("assert check box with id 'x' is checked");
            driver.ExecuteCommand("set check box  with id 'x' to unchecked");
            driver.ExecuteCommand("assert check box  with id 'x' is unchecked");
            driver.ExecuteCommand("set check box  with id 'x' to checked");
            driver.ExecuteCommand("assert check box  with id 'x' is checked");

        }

        [Test]
        public void RadioTests()
        {
            var html = @"
<html><body>
<input type='radio' id='one' name='r' checked='' value='one' />
<input type='radio' id='two' name='r' checked='true' value='two' />
</body>
</html>";

            var url = CreateTempFile(html);

            driver.ExecuteCommand("go to " + url);
            driver.ExecuteCommand("assert radio with id 'one' is not checked");
            driver.ExecuteCommand("assert radio with id 'two' is checked");
           // driver.ExecuteCommand("assert radio with name 'r' value is 'two'"); // TODO: make this work
            driver.ExecuteCommand("set radio  with id 'one' to checked");
            driver.ExecuteCommand("assert radio with id 'one' is checked");
            driver.ExecuteCommand("assert radio with id 'two' is not checked");
            // driver.ExecuteCommand("assert radio with name 'r' value is 'one'"); // TODO: make this work
        }

        [Test]
        public void SelectTests()
        {
            var html = @"
<select id='y'>
    <option id='opt1' value='one' selected='true'>One</option>
    <option id='opt2' value='two'>Two</option>
</select>";

            var url = CreateTempFile(html);

            driver.ExecuteCommand("go to " + url);
            driver.ExecuteCommand("assert select with id 'y' has value 'one'");
            driver.ExecuteCommand("assert option with id 'opt1' is selected");
            driver.ExecuteCommand("assert option with id 'opt2' is not selected");
            driver.ExecuteCommand("set option with id 'opt2' to selected");
            driver.ExecuteCommand("assert select with id 'y' has value 'two'");
            driver.ExecuteCommand("assert option with id 'opt1' is not selected");
            driver.ExecuteCommand("assert option with id 'opt2' is selected");
           // driver.ExecuteCommand("set option with id 'opt2' to not selected");
           // driver.ExecuteCommand("assert select with id 'y' has value ''");

        }

    }
}
