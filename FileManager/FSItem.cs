using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    class FSItem
    {
        protected string name;
        protected MDirectory parent;
        public virtual MDirectory getFolder() 
        {
            return null;
        }
        public virtual string getFullName
        {
            get { return name; }
        }
        public virtual string getName
        {
            get { return name; }
        }
        public MDirectory getParent
        {
            get { return parent; }
        }
    }
}
