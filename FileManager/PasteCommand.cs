using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class PasteCommand: Command
    {
        History history;
        public PasteCommand(History history)
        {
            this.history = history;
        }
        public bool execute(FSItem item) { return history.paste(item); }
        public bool unExecute(FSItem item) { throw new Exception("Trying to unPaste"); }
    }
}
