using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using FluentAssertions;

namespace MC.Selenium.DSL.Tests
{
    

    [TestFixture]
    class GrammarTests
    {
        [Test]
        public void GoToTest()
        {
            var result = Grammar.ParseGoToCommand.TryParse("go to google.com");

            result.WasSuccessful.Should().BeTrue(result.Message);

            var cmd = result.Value as GoToCommand;

            cmd.Should().NotBeNull("should parse go to command");

            cmd.URL.Should().Be("http://google.com");

        }


        // declare 'home page' as element with id 'asdf'
        // set home page to 'x'


        public IEnumerable<String> ClearElementTestFactory()
        {
            yield return @"clear element with id ""gbqfq""";
            yield return @"clear element with name 'q'";
            yield return @"clear element named 'q'";
            yield return @"send 'C6E2E7D1-1D7B-4390-9E7C-4626B18CC137' to element with name 'q'"; 
            yield return @"send 'asdf' to element with name 'q'";
            yield return @"send 'asdf' to element named 'q'";
            yield return @"click element with css selector '#gbqfb'";
            yield return @"click element with xpath ""//button[@id='gbqfb']""";
            yield return @"click element with link text 'asdf'";
           
            yield return @"set element with name 'asdf' text to '1234'";

            yield return "set text box with id '10' to '1234'";
            yield return "set text area with id '10' to '1234'";
            yield return "set radio button group with id '10' to 'x'";
            yield return "set radio button with id '10' to 'x'";
            yield return "set radio with id '10' to 'x'";

            yield return "set check box with id 'xyz' to checked";
            yield return "set check box with id 'xyz' to unchecked";
            yield return "set check box with id 'xyz' to not checked";

            yield return @"set option with id 'opt2' to selected"; 
            yield return @"set option with id 'opt2' to unselected"; 
            yield return @"set option with id 'opt2' to not selected";
            // TODO: work here

            
            yield return "set select with id 'xyz' to 'option2'"; // todo: check single sel vs. multi sel
            //yield return "set select with id 'xyz' to 'option2' and 'option3' and 'option4'";
            //yield return "set select with id 'xyz' to 'option2', 'option3' and 'option4'";

            //yield return "add 'option2' to select with id 'xyz'";
            //yield return "remove 'option2' from select with id 'xyz'";
            //yield return "clear select with id 'xyz'";
        }

        [Test, TestCaseSource("ClearElementTestFactory")]
        public void WebElementCommandTest(String command)
        {
            var result = Grammar.ParseWebElementCommand.TryParse(command);          
            result.WasSuccessful.Should().BeTrue(result.Message);
            result.Remainder.AtEnd.Should().BeTrue("should parse whole line, but stopped on: " + (result.Remainder.AtEnd ? ' ': result.Remainder.Current));
        }

        public IEnumerable<String> AssertCommandTestFactory()
        {
            yield return "assert element with id '10' has attribute 'x'";
            yield return "assert that element with id '10' has attribute 'x'";
            yield return "assert that element with id '10' has value 'x'";

            yield return "assert that element with id '10' has value that ends with 'x'";
            yield return "assert that element with id '10' has value ending with 'x'";
            yield return "assert that element with id '10' has value that begins with 'x'";
            yield return "assert that element with id '10' has value beginning with 'x'";
            yield return "assert that element with id '10' has value that contains 'x'";
            yield return "assert that element with id '10' has value containing 'x'";

            yield return "assert element with id '10' has attribute 'x' that ends with 'x' ";
            yield return "assert element with id '10' has attribute 'x' that contains 'asdf' ";
            yield return "assert element with id '10' has attribute 'x' ending with 'x' ";
            yield return "assert element with id '10' has attribute 'x' containing 'asdf' ";
            yield return "assert element with id '10' has attribute 'x' with value 'x' ";

            yield return "assert element with id '10' has css key 'x'";
            yield return "assert element with id '10' has css key 'x' that ends with 'x' ";
            yield return "assert element with id '10' has css key 'x' that contains 'asdf' ";
            yield return "assert element with id '10' has css key 'x' ending with 'x' ";
            yield return "assert element with id '10' has css key 'x' containing 'asdf' ";
            yield return "assert element with id '10' has css key 'x' with value 'x' ";

            yield return "assert element with id '10' has text with value 'x'";
            yield return "assert element with id '10' has text that ends with 'x' ";
            yield return "assert element with id '10' has text that contains 'asdf' ";
            yield return "assert element with id '10' has text ending with 'x' ";
            yield return "assert element with id '10' has text containing 'asdf' ";

            yield return "assert text box with id '10' has text with value '1234'";
            yield return "assert text box with id '10' has value ending with '1234'";
            yield return "assert text box with id '10' has value '1234'";
            yield return "assert text box with id '10' has text ending with '1234'";
            yield return "assert text area with id '10' has text with value '1234'";
            yield return "assert radio button group with id '10' has text with value 'x'";



            yield return "assert check box with id 'xyz' has attribute 'checked' with value 'true'";
            yield return "assert check box with id 'xyz' is checked";
            yield return "assert check box with id 'xyz' is unchecked";
            yield return "assert check box with id 'xyz' is not checked";

            // TODO: work here

            yield return "assert select with id 'xyz' has value 'option2'";
            yield return "assert select list with id 'xyz' has value 'option2'";

            yield return "assert option with id 'opt1' is selected";
            yield return "assert option with id 'opt1' is not selected";
            yield return "assert option with id 'opt1' is unselected";

            //yield return "assert select with id 'xyz' has option named 'xx' with text 'option2'";
            //yield return "assert select with id 'xyz' value two is 'option2'";
            //yield return "assert select with id 'xyz' has value 'option2'";
            //yield return "assert select with id 'xyz' has 2 values";
            //yield return "assert select with id 'xyz' has three values";
            //yield return "assert select with id 'xyz' values are 'option2' and 'option3' and 'option4'";
            //yield return "assert select with id 'xyz' has values 'option2' and 'option3' and 'option4'";

            //yield return "assert select with id 'xyz' contains values 'option2' and 'option4'";
            //yield return "assert select with id 'xyz' has values 'option2', 'option3' and 'option4'";

        }

        [Test, TestCaseSource("AssertCommandTestFactory")]
        public void AssertCommandTest(String command)
        {
            var result = Grammar.ParseAssertCommand.TryParse(command);
            result.WasSuccessful.Should().BeTrue(result.Message);
            result.Remainder.AtEnd.Should().BeTrue("string should be fully parsed but stopped at: '" + 
                (result.Remainder.AtEnd? ' ': result.Remainder.Current ) + 
                "' on column " + (result.Remainder.AtEnd? -1: result.Remainder.Column) +
                " on line " + (result.Remainder.AtEnd? -1 : result.Remainder.Line));
        }

        public IEnumerable<String> UrlTestFactory()
        {
            yield return "http://localhost";
            yield return "http://127.0.0.1";
            yield return "http://127.0.0.1:80";
            yield return "http://www.google.com";
            yield return "www.google.com";
            yield return "google.com";
            yield return "http://google.com";
            yield return "file:///C:/temp/test.htm";
        }

        [Test, TestCaseSource("UrlTestFactory")]
        public void URLTest(String url)
        {
            var result = Grammar.ParseUrl.TryParse(url);

            result.WasSuccessful.Should().BeTrue("'Failure: " + url + " could not be resolved because " +result.Message + " '");

        }

        // todo
        /*
         set base url as 'google.com'
         go to baseurl + '/asf/asdf/asdf/asdf.aspx'
         */

        public IEnumerable<String> ThenTestFactory()
        {
            yield return @"
go to www.google.com 
then clear element named 'q'
then send 'asdf' to element named 'q'";

            yield return @"go to www.google.com then clear element named 'q' then send 'asdf' to element named 'q'";

            yield return @"
go to www.google.com 
clear element named 'q'
send 'asdf' to element named 'q'";

            yield return @"go to www.google.com clear element named 'q' send 'asdf' to element named 'q'";

            yield return @"go to www.google.com ";

            yield return @"go to http://www.google.com. clear the element named 'q'. ";

            yield return @"
                    go to the http://www.google.com. 
                    clear the element named 'q'. ";

            yield return @"go to www.google.com, clear element named 'q' then send 'asdf' to element named 'q'";

            yield return @"go to www.google.com, clear element named 'q', then send 'asdf' to element named 'q'";
        }

        [Test, TestCaseSource("ThenTestFactory")]
        public void ThenTest(string cmd)
        {
            var result = Grammar.ParseThen.TryParse(cmd);
            result.WasSuccessful.Should().BeTrue(result.Message);  
        }

        public void Sample()
        {
            var command =  @"
go to www.google.com 
then clear element named 'q'
then send 'asdf' to element named 'q'";

            var fireFox = new FireFoxDriver();
        }


        [Test]
        public void SectionTest()
        {
            string command = @"
                go to the http://www.google.com,
                clear the element named 'q'.
                send 'asdf' to the element named 'q'.
                assert the element named 'q' has value 'asdf'.";

            var tests = Grammar.ParseCommandText(command);
        }
    }
}
