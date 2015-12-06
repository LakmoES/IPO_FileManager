using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class MFile: FSItem
    {
        public MFile(string name, MDirectory parent)
        {
            this.name = name;
            this.parent = parent;
        }
        public override string getFullName
        {
            get { return "[F] " + name; }
        }
        public override string getName
        {
            get { return name; }
        }
    }
}
