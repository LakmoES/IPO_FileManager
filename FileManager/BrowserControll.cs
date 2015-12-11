using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class BrowserControll
    {
        List<Command> commandList;
        Stack<KeyValuePair<int, FSItem> > executedCommandList;
        Stack<KeyValuePair<int, FSItem> > canceledCommandList;
        public BrowserControll() 
        {
            commandList = new List<Command>();
            executedCommandList = new Stack<KeyValuePair<int, FSItem> >();
            canceledCommandList = new Stack<KeyValuePair<int, FSItem> >();
        }
        public int addCommand(Command command)
        {
            commandList.Add(command);
            return commandList.IndexOf(command);
        }
        public bool execute(int commandNumber, FSItem item = null, bool ignoreHistory = false)
        {
            bool result = commandList.ElementAt(commandNumber).execute(item);
            if (result && !ignoreHistory)
            {
                executedCommandList.Push(new KeyValuePair<int, FSItem>(commandNumber, item));
                return true;
            }
            else
                if (ignoreHistory && result)
                    return true;
            return false;
        }
        public bool unExecute()
        {
            if (executedCommandList.Count == 0)
                return false;
            bool result = commandList.ElementAt(executedCommandList.Peek().Key).unExecute(executedCommandList.Peek().Value);
            canceledCommandList.Push(executedCommandList.Pop());
            return result;
        }
        public bool reExecute()
        {
            if (canceledCommandList.Count == 0)
                return false;
            bool result = commandList.ElementAt(canceledCommandList.Peek().Key).execute(canceledCommandList.Peek().Value);
            if (result)
                executedCommandList.Push(canceledCommandList.Pop());
            return result;
        }
    }
}
