using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MC.Selenium.DSL.Runner.Model;

namespace MC.Selenium.DSL.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var text = GetText(args);
            Model.Runner runner = new Model.Runner(new ConsoleTestEventObserver());
            runner.Execute(text);
        }

        private static string GetText(string[] args)
        {
            using (var f = File.OpenText(args[0]))
            {
                 var text = f.ReadToEnd();
                 return text;
            }
        }
    }
}
