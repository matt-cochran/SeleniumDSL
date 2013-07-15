using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL.Runner.Model.Tests
{
    [TestFixture]
    public class FunctionalTests
    {
        [Test]
        public void BasicRun()
        {
            string command = @"
test the home page. 
show that the page loads.
use firefox, ie, chrome.
define target page as http://www.google.com.
define search box as element named 'q'.
{
    go to the target page.
    clear the search box.
    send 'asdf' to the search box.
    assert the search box has value 'asdf'
}";

            Runner r = new Runner(new TestExecutor(new TestWebDriverFactory(), new ConsoleTestEventObserver()));
            r.Execute(command);
        }

        public void DoubleRun()
        {
            string command = @"
test the home page. 
show that the page loads.
use firefox, ie, chrome.
define target page as http://www.google.com.
define search box as element named 'q'.
{
    go to the target page.
    clear the search box.
    send 'asdf' to the search box.
    assert the search box has value 'asdf'
}

test the home page. 
show that the page goes well.
use firefox, ie, chrome.
define target page as http://www.google.com.
define search box as element named 'q'.
{
    go to the target page.
    clear the search box.
    send 'asdf' to the search box.
    assert the search box has value 'asdf'
}";

            Runner r = new Runner(new TestExecutor(new TestWebDriverFactory(), new ConsoleTestEventObserver()));
            r.Execute(command);
        }
    }
}
