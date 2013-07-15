using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MC.Selenium.DSL
{
    class CompositeCommand:Command
    {
        private Command[] _Commands;

        public CompositeCommand(IEnumerable<Command> commands)
        {
            // TODO: Complete member initialization
            this._Commands = commands.ToArray();
        }

        public override void ExecuteWith(TestContext context)
        {
            foreach (var item in _Commands)
            {
                item.ExecuteWith(context);
            }
        }
    }
}
