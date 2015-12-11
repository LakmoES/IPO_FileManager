using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace FileManager
{
    class History
    {
        FSItem rootItem;
        FSItem copyBuffer;
        public History(FSItem rootItem)
        {
            this.rootItem = rootItem;
            copyBuffer = null;
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
        public bool delete(FSItem item)
        {
            string fullPath = item.getName;
            FSItem tempItem = item;
            while (tempItem.getParent != null)
            {
                fullPath = tempItem.getParent.getName.Trim('\\') + "\\" + fullPath;
                tempItem = tempItem.getParent;
            }
            Debug.WriteLine("Trying to Delete " + item.getFullName +", path:'" + fullPath + "'");
            if (item.getFolder() != null)
            {
                if (!Directory.Exists(fullPath))
                    throw new DirectoryNotFoundException("Указанная папка не существует: " + item.getName);
                Directory.Delete(fullPath, true);
            }
            else
            {
                if (!File.Exists(fullPath))
                    throw new FileNotFoundException("Указанный файл не существует: " + item.getName , item.getName);
                File.Delete(fullPath);
            }
            return true;
        }
        public void copy(FSItem item)
        {
            copyBuffer = item;
        }
    }
}
