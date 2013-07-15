using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.Selenium.DSL.Runner.Model
{
    public class Runner
    {
        private readonly ITestExecutor _Executor;

        /// <summary>
        /// Initializes a new instance of the Runner class.
        /// </summary>
        /// <param name="_Executor"></param>
        internal Runner(ITestExecutor _Executor)
        {
            this._Executor = _Executor;
            
        }

        public Runner(ITestEventObserver logger) : this(new TestExecutor(new WebDriverFactory(), logger)) { } // cheap IOC

        public void Execute(String command)
        {
            foreach (var test in GrammarParser.ParseCommand(command))
            {
                _Executor.Execute(test);
            }
        }
    }
}
