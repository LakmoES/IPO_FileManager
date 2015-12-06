using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class UpCommand : Command
    {
        History history;
        public UpCommand(History history)
        {
            this.history = history;
        }
        public bool execute(FSItem item) { return history.goUp(); }
        public bool unExecute(FSItem item) { return history.enter(item); }
    }
}
