using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace FileManager
{
    class FileManagerCore
    {
        ListBox lb;
        TextBox tb;
        ComboBox cb;

        BrowserControll browserControll;
        History history;
        int upID, enterID;
        string address;
        public FileManagerCore(ListBox lb, TextBox tb, ComboBox cb) 
        {
            history = new History(FSScan.inDirectory(null, FSScan.getDrives()[0]));
            browserControll = new BrowserControll();
            upID = browserControll.addCommand(new UpCommand(history));
            enterID = browserControll.addCommand(new EnterCommand(history));

            this.lb = lb;
            this.tb = tb;
            this.cb = cb;
            lb.DisplayMember = "getFullName";
            drawInView();

            drawDrives();
        }
        public void changeDrive(string drive)
        {
            history = new History(FSScan.inDirectory(null, drive));
            browserControll = new BrowserControll();
            upID = browserControll.addCommand(new UpCommand(history));
            enterID = browserControll.addCommand(new EnterCommand(history));
            address = "";
            drawInView();
        }
        private void drawDrives()
        {
            foreach(string drive in FSScan.getDrives())
                cb.Items.Add(drive);
            if (cb.Items.Count > 0)
                cb.SelectedIndex = 0;
        }
        private void drawInView() //отрисовка папок и файлов
        {
            if (history.getRootItem.getFolder() == null)
                throw new Exception("File cannot be a root item");
            FSItem tmp = history.getRootItem;
            address = tmp.getName;
            while (tmp.getParent != null)
            {
                address = tmp.getParent.getName.Trim('\\') + "\\" + address;
                tmp = tmp.getParent;
            }
            tb.Text = address;
            lb.Items.Clear();
            foreach (FSItem fsItem in history.getRootItem.getFolder().getChildren)
                lb.Items.Add(fsItem);
        }

        public void goBack()
        {
            if (browserControll.unExecute())
                drawInView();
        }
        public void goForward()
        {
            if(browserControll.reExecute())
                drawInView();
        }
        public void goUp() 
        {
            if(browserControll.execute(upID, history.getRootItem))
                drawInView();
        }
        public void enter(FSItem item)
        {
            Console.WriteLine("Address:"+address);
            if (browserControll.execute(enterID, item))
            {
                FSItem newItem = FSScan.inDirectory(item.getParent, address + "\\" + item.getName);
                item.getFolder().clearChildren();
                foreach (FSItem it in newItem.getFolder().getChildren)
                    item.getFolder().addItem(it);
                drawInView();
            }
        }
    }
}
