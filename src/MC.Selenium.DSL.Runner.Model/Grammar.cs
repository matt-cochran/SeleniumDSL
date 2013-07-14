using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Selenium.DSL.Runner.Model
{
    internal static class Grammar
    {
        public static Test[] ParseCommand(String value)
        {
            var result = ParseTest.Many().End().Parse(value).ToArray();
            return result;
        }

        internal static readonly Parser<String> ParseTarget =
            from sp in Parse.WhiteSpace.Many()
            from test in Parse.String("test")
            from sp1 in Parse.WhiteSpace.Many()
            from the in Parse.String("the").Optional()
            from sp2 in Parse.WhiteSpace.Many()
            from thing in Parse.CharExcept('.').Many().Select(_ => new String(_.ToArray()))
            from period in Parse.Char('.')
            from end in Parse.WhiteSpace.Many()
            select thing;

        internal static readonly Parser<String> ParsePurpose =
            from sp in Parse.WhiteSpace.Many()
            from test in Parse.String("show")
            from sp1 in Parse.WhiteSpace.Many()
            from the in Parse.String("that").Optional()
            from sp2 in Parse.WhiteSpace.Many()
            from thing in Parse.CharExcept('.').Many().Select(_ => new String(_.ToArray()))
            from period in Parse.Char('.')
            from end in Parse.WhiteSpace.Many()
            select thing;

        internal static readonly Parser<String[]> ParseBrowsers =
            from sp in Parse.WhiteSpace.Many()
            from test in Parse.String("use")
            from sp1 in Parse.WhiteSpace.Many()
            from browsers in Parse.CharExcept(",.").Many().Select(_ => new String(_.ToArray()).Trim()).DelimitedBy(Parse.Char(','))
            from sp2 in Parse.WhiteSpace.Many()
            from period in Parse.Char('.')
            from sp3 in Parse.WhiteSpace.Many()
            select browsers.ToArray();


        internal static readonly Parser<KeyValuePair<String, String>> ParseVariable =
            from sp in Parse.WhiteSpace.Many()
            from define in Parse.String("define")
            from sp1 in Parse.WhiteSpace.Many()
            from key in Parse.AnyChar.Until(Parse.String("as"))
                .Select(_ => new String(_.ToArray()).Trim())

            from sp2 in Parse.WhiteSpace.Many()

            from val in MC.Selenium.DSL.Grammar.ParseUrl.Or(
                Parse.CharExcept('.').Many().Select(_ => new String(_.ToArray()))
            )

            //from val in Parse.AnyChar.Until(
            //    from x in Parse.Char('.')
            //    from y in Parse.WhiteSpace
            //    select "found end"
            //).Select(_ => new String(_.ToArray()))
           // from val in Parse.CharExcept('.').Many().Select(_ => new String(_.ToArray()))
            from period in Parse.Char('.')
            from sp3 in Parse.WhiteSpace.Many()
            select new KeyValuePair<String, String>(key, val);

        internal static readonly Parser<String> ParseCommandText =
            from sp in Parse.WhiteSpace.Many()
            from start in Parse.Char('{')
            from mid in Parse.CharExcept('}').Many().Select(_ => new String(_.ToArray()))
            from end in Parse.Char('}')
            from sp2 in Parse.WhiteSpace.Many()
            select mid;


        private static readonly Parser<Test> ParseTest =
            from sp in Parse.WhiteSpace.Many()
            from target in ParseTarget
            from purpose in ParsePurpose
            from browsers in ParseBrowsers.Optional().Select(_ => _.GetOrElse(new String[0]))
            from vars in ParseVariable.Many().Optional().Select(_ => _.GetOrElse(new Dictionary<String, string>()))
            from command in ParseCommandText
            from sp2 in Parse.WhiteSpace.Many()
            select new Test
            {
                Target = target,
                Purpose = purpose,
                Browsers = browsers,
                Variables = vars.ToDictionary(_ => _.Key, _ => _.Value),
                Command = command
            };

        
            
    }
}