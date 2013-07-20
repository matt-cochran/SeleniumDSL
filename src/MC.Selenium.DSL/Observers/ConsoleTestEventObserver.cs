using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Selenium.DSL
{
    public class ConsoleTestEventObserver:ITestEventObserver
    {
        private bool _Passing = true;

        public void Log(TestEventType type, string message)
        {
            Console.ForegroundColor = GetConsoleColor(type);
            Console.BackgroundColor = ConsoleColor.Black;

            if (type == TestEventType.Failure)
            {
                _Passing = false;
            }

            switch(type)
            {
                case TestEventType.Detail:
                    Console.Write("            ");
                    break;
                case TestEventType.BeginOperation:
                    Console.Write("        begin: ");
                    break;
                case TestEventType.EndOperation:          
                    Console.Write("        end: ");
                    break;
                case TestEventType.BeginCommand:
                    Console.Write("begin: ");
                    break;
                case TestEventType.EndCommand:
                    Console.Write("end : ");
                    break;
                default: break;
            }

            Console.WriteLine(message);
            Console.ResetColor();
        }

        private ConsoleColor GetConsoleColor(TestEventType type)
        {
            switch (type)
            {
                case TestEventType.Warning: return ConsoleColor.Yellow;
                case TestEventType.Failure: return ConsoleColor.Red;
                case TestEventType.Detail: return ConsoleColor.Gray;
                case TestEventType.BeginOperation:
                case TestEventType.EndOperation:
                case TestEventType.BeginCommand:
                case TestEventType.EndCommand:
                    return ConsoleColor.Green;
                default: return ConsoleColor.White;
            }
        }


        public bool IsPassing()
        {
            return _Passing;
        }
    }
}
