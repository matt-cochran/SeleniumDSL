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

            if (type == TestEventType.Detail)
            {
                Console.Write("    ");
            }

            Console.WriteLine(message);
        }

        private ConsoleColor GetConsoleColor(TestEventType type)
        {
            switch (type)
            {
                case TestEventType.Warning: return ConsoleColor.Yellow;
                case TestEventType.Failure: return ConsoleColor.Red;
                case TestEventType.Detail: return ConsoleColor.Gray;
                default: return ConsoleColor.White;
            }
        }


        public bool IsPassing()
        {
            return _Passing;
        }
    }
}
