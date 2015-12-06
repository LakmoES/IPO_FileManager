using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    interface Command
    {
        bool execute(FSItem item);
        bool unExecute(FSItem item);
    }
}
