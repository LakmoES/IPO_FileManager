using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class DeleteCommand : Command
    {
        History history;
        public DeleteCommand(History history)
        {
            this.history = history;
        }
        public bool execute(FSItem item) { return history.delete(item); }
        public bool unExecute(FSItem item) { throw new Exception("Trying to unDelete"); }
    }
}
