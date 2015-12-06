using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class MDirectory:FSItem
    {
        List<FSItem> items;
        public MDirectory(string name, MDirectory parent)
        {
            this.name = name;
            this.parent = parent;
            items = new List<FSItem>();
        }
        public override MDirectory getFolder()
        {
            return this;
        }
        public void addItem(FSItem item)
        {
            items.Add(item);
        }
        public override string getFullName
        {
            get { return "[D] " + name; }
        }
        public override string getName
        {
            get { return name; }
        }
        public FSItem[] getChildren
        { get { return items.ToArray(); } }
        public void clearChildren()
        {
            this.items.Clear();
        }
    }
}
