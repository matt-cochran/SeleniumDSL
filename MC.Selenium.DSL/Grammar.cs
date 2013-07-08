﻿using OpenQA.Selenium;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MC.Selenium.DSL
{
    public static class Grammar
    {
        internal static MC.Selenium.DSL.Command ParseCommandText(string command)
        {
            var c = ParseThen.End().Parse(command);
            return c;
        }

        public static void ExecuteCommand(this IWebDriver driver, String command)
        {
            var c = ParseCommandText(command);
            c.ExecuteWith(driver);
        }

        internal static readonly Parser<String> ParseHttp =
            from leading in Parse.WhiteSpace.Many()
            from http in Parse.String("http")
            from s in Parse.String("s").Optional().Select(_ => _.GetOrElse(String.Empty))
            from trailing in Parse.String("://").Return("://")
            select new String(http.ToArray()) + s + trailing;

        internal static readonly Parser<String> ParseIP =
            from leading in Parse.WhiteSpace.Many()
            from one in Parse.Numeric.AtLeastOnce()
            from dot1 in Parse.Char('.')
            from two in Parse.Numeric.AtLeastOnce()
            from dot2 in Parse.Char('.')
            from three in Parse.Numeric.AtLeastOnce()
            from dot3 in Parse.Char('.')
            from four in Parse.Numeric.AtLeastOnce()
            from trailing in Parse.WhiteSpace.Many()
            select one + "." + two + "." + three + "." + four;

        internal static readonly Parser<String> ParseDomainPart =
            from x in Parse.LetterOrDigit.AtLeastOnce().Select(_ => new String(_.ToArray()))
            from y in Parse.String(".").Return(".")
            select x + y;

        internal static readonly Parser<String> ParseDomain =
            from leading in Parse.WhiteSpace.Many()
            from one in ParseDomainPart
            from two in ParseDomainPart.Optional().Select(_ => _.GetOrElse(String.Empty))
            from three in Parse.LetterOrDigit.AtLeastOnce().Select(_ => new String(_.ToArray()))
            from trailing in Parse.WhiteSpace.Many()
            select one + two + three;

        internal static readonly Parser<String> ParsePort =
            from colon in Parse.Char(':')
            from num in Parse.Digit.AtLeastOnce().Select(_ => new String(_.ToArray()))
            select colon + num;

        internal static readonly Parser<String> ParseWebUrl =
            from leading in Parse.WhiteSpace.Many()
            from http in ParseHttp.Optional().Select(_ => _.GetOrElse("http://"))
            from uri in Parse.String("localhost").Or(ParseIP).Or(ParseDomain).Select(_ => new String(_.ToArray()))
            from port in ParsePort.Optional().Select(_ => _.GetOrElse(String.Empty))
            from trailing in Parse.WhiteSpace.Many()
            select http + uri + port;

        internal static readonly Parser<String> ParseLocalUrl =
            from leading in Parse.WhiteSpace.Many()
            from file in Parse.String("file:///").Return("file:///")
            from path in Parse.LetterOrDigit.Or(Parse.Chars("/:-.")).Many().Select(_ => new String(_.ToArray()))
            from trailing in Parse.WhiteSpace.Many()
            select file + path;

        public static readonly Parser<String> ParseUrl = ParseWebUrl.Or(ParseLocalUrl);

        internal static Parser<String> ParseWordBase(String w, bool optional = false)
        {
            if (optional)
            {
                return
                    from p in Parse.WhiteSpace.Many()
                    from s in Parse.String(w)
                                    .Select(_ => new String(_.ToArray()))
                                    .Optional()
                                    .Select(_ => _.GetOrElse(String.Empty))
                    from t in Parse.WhiteSpace.Many()
                    select s;
            }

            return
                from p in Parse.WhiteSpace.Many()
                from s in Parse.String(w)
                                .Select(_ => new String(_.ToArray()))
                from t in Parse.WhiteSpace.Many()
                select s;
        }

        internal static Parser<String> ParseWord(String w)
        {
            return ParseWordBase(w);
        }

        internal static Parser<String> ParseOptionalWord(String w)
        {
            return ParseWordBase(w, true);
        }

        internal static Parser<String> ParseWords(params String[] w)
        {
            return ParseWordsBase(false, w);
        }

        internal static Parser<String> ParseOptionalWords(params string[] w)
        {
            return ParseWordsBase(true, w);
        }

        private static Parser<String> ParseWordsBase(bool optional, params String[] w)
        {
            Parser<String> core = null;

            foreach(var word in w)
            {
                if(core == null)
                {
                    core = ParseWord(word);
                }
                else
                {
                    core = core.ThenWord(word);
                }
            }

            return core;
        }

        internal static Parser<String> ThenWordBase(this Parser<String> target, String next, bool optional = false)
        {
            return target.Then(_ => ParseWordBase(next, optional).Select(x => String.Format("{0} {1}", x, _)));
        }

        internal static Parser<String> ThenWord(this Parser<String> target, String next)
        {
            return ThenWordBase(target, next);
        }

        internal static Parser<String> ThenOptionalWord(this Parser<String> target, String next)
        {
            return ThenWordBase(target, next, true);
        }

        internal static readonly Parser<Command> ParseGoToCommand =
            from x in ParseWords("go", "to").ThenOptionalWord("the")
            from url in ParseUrl
            from trailing in Parse.WhiteSpace.Many()
            select new GoToCommand(url);

        internal static readonly Parser<String> ParseQuoted =
            from leading in Parse.WhiteSpace.Many()
            from value in 
            (
                from left in Parse.String("'")
                from middle in Parse.CharExcept('\'').Many().Select(_ => new string(_.ToArray()))
                from end in Parse.String("'")
                select middle)
            .Or(
                from left in Parse.String(@"""")
                from middle in Parse.CharExcept(@"""").Many().Select(_ => new string(_.ToArray()))
                from end in Parse.String(@"""")
                select middle
            )
            from trailing in Parse.WhiteSpace.Many()
            select value;

        internal static readonly Parser<Action<IWebElement>> ParseElement =
            from the in ParseOptionalWord("the")
            from x in
                (from element in ParseWord("element") select Do.Nothing)                                    // element
                .Or(from w in ParseWords("text", "area") select Do.AssertTagIsTextArea)             // <textarea>
                .Or(from w in ParseWords("text", "box") select Do.AssertTagIsTextInput)             // <input type="text">
                .Or(from w in ParseWord("radio").ThenOptionalWord("button").ThenOptionalWord("group") select Do.AssertTagIsRadioInput)// <input type="radio" name="group1" value="Option 1"> Option 1</input>
                .Or(from w in ParseWords("check", "box") select Do.AssertTagIsCheckBoxInput)        // <input type="checkbox"                
                .Or(from w in ParseWord("select").ThenOptionalWord("list") select Do.AssertTagIsSelectInput)  //<select id="select1">
                .Or(from area in ParseWord("option") select Do.AssertTagIsOption)                           //<option id="opt1">
            select x;

        //send 'asdf' to element with name 'q'
        internal static readonly Parser<Action<IWebElement>> ParseElementAction =
            (from w in ParseWord("clear") select Do.Clear)// clear
            .Or(from w in ParseWord("click") select Do.Click)// click
            .Or( // send 'asdf' to 
                from snd in ParseWord("send")
                from quoted in Grammar.ParseQuoted
                from t in ParseWord("to")
                select new Action<IWebElement>(_ => _.SendKeys(quoted)))
            ;

        internal static readonly Parser<By> ParseBy =
            (from w in ParseWords("with", "id") from value in ParseQuoted select By.Id(value))
            .Or(from w in ParseWords("with", "name") from value in ParseQuoted select By.Name(value))
            .Or(from w in ParseWord("named") from value in ParseQuoted select By.Name(value))
            .Or(from w in ParseWords("with", "css", "selector") from value in ParseQuoted select By.CssSelector(value))    //"click element with css selector \'#gbqfb\'"
            .Or(from w in ParseWords("with", "xpath") from value in ParseQuoted select By.XPath(value))                             // @"click element with xpath ""//button[@id='gbqfb']"""
            .Or(from w in ParseWords("with", "link", "text") from value in ParseQuoted select By.LinkText(value));         // @"click element with link text 'asdf'"

        internal static Parser<Func<IWebElement, String>> ParseHasAttribute =
            from w in ParseWords("has", "attribute")
            from quoted in Grammar.ParseQuoted
            from sp3 in Parse.WhiteSpace.Many()
            select new Func<IWebElement, String>(_ => _.GetAttribute(quoted));

        internal static Parser<Func<IWebElement, String>> ParseHasCssKey =
            from w in ParseWords("has", "css", "key")
            from quoted in Grammar.ParseQuoted
            from sp3 in Parse.WhiteSpace.Many()
            select new Func<IWebElement, String>(_ => _.GetCssValue(quoted));

        internal static Parser<Func<IWebElement, String>> ParseHasText =
           from has in ParseWord("has")
           from textOrValue in ParseWord("text").Or(ParseWord("value"))
           select new Func<IWebElement, String>(_ => _.Text);

        internal static Parser<Action<String>> ParseEndsWith =
            (
                from w in ParseWords("that", "ends", "with")
                from q in Grammar.ParseQuoted
                from sp4 in Parse.WhiteSpace.Many()
                select new Action<String>(_ => _.EndsWith(q)))
              .Or(
                  from w in ParseWords("ending", "with")
                  from q in Grammar.ParseQuoted
                  from sp4 in Parse.WhiteSpace.Many()
                  select new Action<String>(_ => _.EndsWith(q))
              );

        internal static Parser<Action<String>> ParseBeginsWith =
           (
               from w in ParseWords("that", "begins", "with")
               from q in Grammar.ParseQuoted
               from sp4 in Parse.WhiteSpace.Many()
               select new Action<String>(_ => _.StartsWith(q)))
             .Or(
                from w in ParseWords("beginning", "with")
                from q in Grammar.ParseQuoted
                from sp4 in Parse.WhiteSpace.Many()
                select new Action<String>(_ => _.StartsWith(q))
             );

        internal static Parser<Action<String>> ParseContains =
            (
                from w in ParseWords("that", "contains")
                from q in Grammar.ParseQuoted
                from sp4 in Parse.WhiteSpace.Many()
                select new Action<String>(_ => _.Contains(q))
            )
            .Or(
                from w in ParseWord("containing")
                from q in Grammar.ParseQuoted
                from sp4 in Parse.WhiteSpace.Many()
                select new Action<String>(_ => _.Contains(q))
                );

        internal static Parser<Action<String>> ParseWithValue =
                from w in ParseWords("with", "value")
                from q in Grammar.ParseQuoted
                from sp4 in Parse.WhiteSpace.Many()
                select new Action<String>(_ => _.Equals(q));

        //has attribute 'x'
        //assert element with id '10' has attribute 'x' that ends with 'x'
        internal static readonly Parser<Action<IWebElement>> ParseAssertion =
            (
                from doGet in Grammar.ParseHasAttribute.Or(Grammar.ParseHasCssKey)
                from doTest in
                    Grammar.ParseEndsWith.Or(
                    Grammar.ParseContains).Or(
                    Grammar.ParseWithValue).Or(
                    Grammar.ParseBeginsWith).Or(
                    Parse.WhiteSpace.Many().Return(new Action<String>(_ => { }))) // checking for existance with no additional constraints
                select new Action<IWebElement>(_ => doTest(doGet(_)))
            )
            .Or(
                from doGet in Grammar.ParseHasText
                from doTest in
                    Grammar.ParseEndsWith.Or(
                    Grammar.ParseContains).Or(
                    Grammar.ParseWithValue).Or(
                    Grammar.ParseBeginsWith).Or(
                    Grammar.ParseQuoted.Select(_ => new Action<String>(x => x.Equals(_)))).Or(
                    Parse.WhiteSpace.Many().Return(new Action<String>(_ => { }))) // checking for existance with no additional constraints
                select new Action<IWebElement>(_ => doTest(doGet(_)))
            ).Or(
                from w in ParseWord("is")
                from c in ParseWord("checked").Return(Do.AssertIsChecked)
                    .Or(Grammar.ParseNotChecked.Return(Do.AssertNotChecked))
                    .Or(ParseWord("selected").Return(Do.AssertIsSelected))
                    .Or(Grammar.ParseNotSelected.Return(Do.AssertNotSelected))
                from sp2 in Parse.WhiteSpace.Many()
                select c
            );

        internal static readonly Parser<String> ParseNotChecked =
            Parse.String("unchecked").Select(_ => new String(_.ToArray()))
            .Or(from w in ParseWords("not", "checked") select "unchecked");

        internal static readonly Parser<String> ParseNotSelected =
            Parse.String("unselected").Select(_ => new String(_.ToArray()))
            .Or(from w in ParseWords("not", "selected") select "unselected");

        //"assert element with id '10' has attribute 'x'"
        internal static readonly Parser<WebElementCommand> ParseAssertCommand =
            from w in ParseWord("assert").ThenOptionalWord("that")
            from element in Grammar.ParseElement
            from sp3 in Parse.WhiteSpace.Many()
            from b in Grammar.ParseBy
            from sp4 in Parse.WhiteSpace.Many()
            from doAssert in ParseAssertion
            select new WebElementCommand(b, doAssert);


        internal static readonly Parser<Command> ParseWebElementCommand =
            (
            //"clear element with id 'gbqfq'"
            //"clear element with name 'q'"
                from doAction in ParseElementAction
                from sp in Parse.WhiteSpace.Many()
                from prefix in ParseElement
                from b in ParseBy
                from trailing in Parse.WhiteSpace.Many()
                select new WebElementCommand(b, doAction))
            .Or(
            //"set element with id 'asdf' text to"
                from w in ParseWord("set")
                from doCheck in ParseElement
                from b in ParseBy
                from textto in ParseOptionalWord("text").ThenWord("to")
                from doAction in Grammar.ParseQuoted.Select(_ => Do.Clear.Then(Do.SendKeys(_)))
                    .Or(ParseWord("checked").Select(_ => Do.Check))
                    .Or(Grammar.ParseNotChecked.Select(_ => Do.UnCheck))
                    .Or(ParseWord("selected").Select(_ => Do.Select))
                    .Or(Grammar.ParseNotSelected.Select(_ => Do.UnSelect))
                from sp6 in Parse.WhiteSpace.Many()
                select new WebElementCommand(b, doCheck.Then(doAction))
            );

        internal static readonly Parser<Command> ParseCommand =
            Grammar.ParseGoToCommand
            .Or(Grammar.ParseAssertCommand)
            .Or(Grammar.ParseWebElementCommand);

        internal static readonly Parser<Command> ParseCommandThen =
            from sp0 in Parse.WhiteSpace.Many()
            from cmd in ParseCommand
            from spx in Parse.WhiteSpace.Many()
            from comma in Parse.Chars(",.").Optional()
            from then in ParseOptionalWord("then")
            select cmd;

        internal static readonly Parser<Command> ParseThen =
            ParseCommandThen.AtLeastOnce().Select(_ => new CompositeCommand(_));

    }
}
