using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class MDirectoryAdapter : Measured
    {
        private MDirectory adaptee;
        public MDirectoryAdapter(MDirectory adaptee)
        {
            this.adaptee = adaptee;
        }
        public long getSize
        {
            get
            {
                long size = 0;
                foreach (long s in (adaptee as MDirectory).getChildrenSize)
                    size += s;
                return size;
            }
        }
    }
}