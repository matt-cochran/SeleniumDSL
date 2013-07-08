SeleniumDSL
===========

DSL for executing Selenium tests


Overview: In this C# project I am working on a way to make maintenance of automated browser tests easier for web applications.  

When the code in  a project matures, the amount of tests grows and changes become more complex and potentially have a broader impact.  As a developer I want to be able to offload some of the test maintenance and creation to the qa and/or business so I can spend more time on building cool new features.  Possible opportunities would be the high-level tests like of defining critical paths through the site or new features.  These tests that will probably live the longest because they have the least coupling with implementation.  Some of the more technical tests that are a bit closer to the metal will always be maintained by developers, but you don't need to be a developer to understand the high-level business requirements and script basic tests against the application.

Goal: This project's goal is to create tests in a language at a higher level than a programming language where tests can be written and understood in a syntax closer to english.

Here is the syntax for a test scenario:

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
