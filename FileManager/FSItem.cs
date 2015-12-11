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
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            FSItem item = obj as FSItem;

            if (!item.getFullName.Equals(this.getFullName))
                return false;

            FSItem thisTmp = this;
            while(thisTmp.getParent!=null)
            {
                if (!thisTmp.getParent.Equals(item))
                    return false;
                thisTmp = thisTmp.getParent;
                item = item.getParent;
            }
            return true;
        }
    }
}
