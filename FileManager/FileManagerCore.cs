using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    class FileManagerCore
    {
        ListBox lb;
        TextBox tb;
        ComboBox cb;

        FSItem buffer;
        bool cutFlag;

        BrowserControll browserControll;
        History history;
        int upID, enterID, deleteID, pasteID;
        string address;
        public FileManagerCore(ListBox lb, TextBox tb, ComboBox cb) 
        {
            history = new History(FSScan.inDirectory(null, FSScan.getDrives()[0]));
            initCommands();

            this.lb = lb;
            this.tb = tb;
            this.cb = cb;

            cutFlag = false;

            lb.DisplayMember = "getFullName";
            drawInView();

            drawDrives();
        }
        private void initCommands()
        {
            browserControll = new BrowserControll();
            upID = browserControll.addCommand(new UpCommand(history));
            enterID = browserControll.addCommand(new EnterCommand(history));
            deleteID = browserControll.addCommand(new DeleteCommand(history));
            pasteID = browserControll.addCommand(new PasteCommand(history));
        }
        public void changeDrive(string drive)
        {
            history = new History(FSScan.inDirectory(null, drive));
            initCommands();
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

        public FSItem getBuffer
        {
            get
            {
                return buffer;
            }
        }

        public bool getCutFlag
        {
            get
            {
                return cutFlag;
            }
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
            if (browserControll.execute(enterID, item))
            {
                FSItem newItem = FSScan.inDirectory(item.getParent, address + "\\" + item.getName);
                item.getFolder().clearChildren();
                foreach (FSItem it in newItem.getFolder().getChildren)
                    item.getFolder().addItem(it);
                drawInView();
            }
        }
        public void delete(FSItem item)
        {
            if (browserControll.execute(deleteID, item, true))
            {
                FSItem rootItem = FSScan.inDirectory(history.getRootItem.getParent, address);
                FSItem[] children = rootItem.getFolder().getChildren;
                history.getRootItem.getFolder().clearChildren();
                foreach (FSItem it in children)
                    history.getRootItem.getFolder().addItem(it);
                drawInView();
            }
        }
        public long size(FSItem item)
        {
            Measured measured;
            if (item.getFolder() == null)
                measured = item as Measured;
            else
                measured = new MDirectoryAdapter(item as MDirectory);
            return measured.getSize;
        }
        public void paste(FSItem item)
        {
            Console.WriteLine(String.Format("FMC paste '{0}'", item.getName));
            buffer = null;
            cutFlag = false;

            if (browserControll.execute(pasteID, item, true))
                history.getRootItem.getFolder().addItem(item);
            drawInView();
        }
        public void cut(FSItem item)
        {
            Console.WriteLine(String.Format("FMC cut '{0}'", item.getName));
            buffer = item;
            cutFlag = true;
        }
        public void copy(FSItem item)
        {
            Console.WriteLine(String.Format("FMC copy '{0}'", item.getName));
            buffer = item;
            cutFlag = false;
        }
    }
}
