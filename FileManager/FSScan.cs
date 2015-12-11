using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    class FSScan
    {
        public static FSItem inDirectory(MDirectory parent, string path)
        {
            MDirectory rootFolder = new MDirectory(new DirectoryInfo(path).Name, parent);

            DirectoryInfo dir = new DirectoryInfo(path);
            //============Список каталогов=============
            foreach (var item in dir.GetDirectories())
                rootFolder.addItem(new MDirectory(item.Name, rootFolder));
            //==============Список файлов==============
            foreach (var item in dir.GetFiles())
                rootFolder.addItem(new MFile(item.Name, rootFolder));

            return rootFolder;
        }
        public static string[] getDrives()
        {
            string[] drives = Environment.GetLogicalDrives();
            return drives;
        }
    }
}
