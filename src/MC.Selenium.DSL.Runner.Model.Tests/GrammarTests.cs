using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Sprache;

namespace MC.Selenium.DSL.Runner.Model.Tests
{

    [TestFixture]
    public class GrammarTests
    {
        [Test]
        public void TargetTest()
        {
            var command = "test the home page.";
            var result = GrammarParser.ParseTarget.End().Parse(command);
            result.Should().Be("home page");
        }

        [Test]
        public void PurposeTest()
        {
            var command = "show that the page loads.";
            var result = GrammarParser.ParsePurpose.End().Parse(command);
            result.Should().Be("the page loads");
        }

        [Test]
        public void BrowserTest()
        {
            var command = "use firefox, ie, chrome.";
            var result = GrammarParser.ParseBrowsers.End().Parse(command);
            result.Length.Should().Be(3);
            result[0].Should().Be("firefox");
            result[1].Should().Be("ie");
            result[2].Should().Be("chrome");
        }

        [Test]
        public void VariableTest()
        {
            var command = @"define target page as http://www.google.com.";

            var result = GrammarParser.ParseVariable.End().Parse(command);
            result.Key.Should().Be("target page");
            result.Value.Should().Be("http://www.google.com");
        }

        [Test]
        public void Variable2Test()
        {
            var command = @"define search box as element named 'q'.";

            var result = GrammarParser.ParseVariable.End().Parse(command);
            result.Key.Should().Be("search box");
            result.Value.Should().Be("element named 'q'");
        }


        [Test]
        public void BasicLayoutTest()
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
            var tests = GrammarParser.ParseCommand(command);

            tests.Length.Should().Be(1);

            var test = tests[0];

            test.Browsers.Count().Should().Be(3);
            test.Browsers[0].Should().Be("firefox");
            test.Browsers[1].Should().Be("ie");
            test.Browsers[2].Should().Be("chrome");
            var lines = test.Command.Trim().Split('\n');

            lines.Length.Should().Be(4);

            lines[0].Trim().Should().Be("go to the target page.");
            lines[1].Trim().Should().Be("clear the search box.");
            lines[2].Trim().Should().Be("send 'asdf' to the search box.");
            lines[3].Trim().Should().Be("assert the search box has value 'asdf'");

            test.Purpose.Should().Be("the page loads");
            test.Target.Should().Be("home page");
            test.Variables.Count().Should().Be(2);

            test.Variables.Keys.Contains("target page").Should().BeTrue();
            test.Variables.Keys.Contains("search box").Should().BeTrue();

            test.Variables["target page"].Should().Be("http://www.google.com");
            test.Variables["search box"].Should().Be("element named 'q'");
        }
    }
}
