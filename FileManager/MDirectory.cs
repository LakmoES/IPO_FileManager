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
        public long[] getChildrenSize
        {
            get
            {
                string path = name;
                MDirectory parent = this.parent;
                while(parent != null)
                {
                    path = parent.getName + "\\" + path;
                    parent = parent.parent;
                }
                items = FSScan.inDirectory(this.parent, path).getFolder().getChildren.ToList();
                List<long> size = new List<long>();
                foreach (FSItem item in items)
                {
                    if (item.getFolder() == null) //it's a file
                        size.Add((item as MFile).getSize);
                    else //it's a dir
                        size.AddRange((item as MDirectory).getChildrenSize);
                }
                return size.ToArray();
            }
        }
    }
}
