using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class EnterCommand : Command
    {
        History history;
        public EnterCommand(History history)
        {
            this.history = history;
        }
        public bool execute(FSItem item) { return history.enter(item); }
        public bool unExecute(FSItem item) { return history.enter(item.getParent); }
    }
}
