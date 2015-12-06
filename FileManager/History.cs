using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FileManager
{
    class History
    {
        FSItem rootItem;
        public History(FSItem rootItem)
        {
            this.rootItem = rootItem;
        }
        public FSItem getRootItem
        {
            get { return rootItem; }
        }
        public void goBack() { Debug.WriteLine("Go Back History."); }
        public bool goUp() 
        {
            if (rootItem.getParent != null)
            {
                rootItem = rootItem.getParent;
                Debug.WriteLine("GoUp.");
                return true;
            }
            else
            {
                Debug.WriteLine("GoUp Fail. NULL-Parent");
                return false;
            }
        }
        public bool enter(FSItem item) 
        { 
            if (item.getFolder() != null)
            {
                rootItem = item;
                return true;
            }
            else
            {
                Debug.WriteLine("Open file: " + item.getName);

                FSItem tmp = item;
                string path = tmp.getName;
                while (tmp.getParent != null)
                {
                    path = tmp.getParent.getName.Trim('\\') + "\\" + path;
                    tmp = tmp.getParent;
                }
                Process.Start(path);

                return false;
            }
        }
    }
}
