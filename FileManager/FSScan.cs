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
        /*public static FSItem inDirectory()
        {
            MDirectory rootFolder = new MDirectory("root", null);
            rootFolder.addItem(new MFile("item1", rootFolder));
            MDirectory folder1 = new MDirectory("item2", rootFolder);
            rootFolder.addItem(folder1);
            folder1.addItem(new MFile("item2.1", folder1));
            folder1.addItem(new MDirectory("item2.2", folder1));

            return rootFolder;
        }*/
        public static FSItem inDirectory(MDirectory parent, string path)
        {
            MDirectory rootFolder = new MDirectory(new DirectoryInfo(path).Name, parent);

            DirectoryInfo dir = new DirectoryInfo(path);
            //Console.WriteLine("============Список каталогов=============");
            foreach (var item in dir.GetDirectories())
            {
                //Console.WriteLine(item.Name);
                rootFolder.addItem(new MDirectory(item.Name, rootFolder));
            }
            //Console.WriteLine("==============Список файлов==============");
            foreach (var item in dir.GetFiles())
            {
                //Console.WriteLine(item.Name);
                rootFolder.addItem(new MFile(item.Name, rootFolder));
            }

            return rootFolder;
        }
        public static string[] getDrives()
        {
            string[] drives = Environment.GetLogicalDrives();
            foreach (string s in drives)
            {
                Console.WriteLine(s);
            }
            return drives;
        }
    }
}
