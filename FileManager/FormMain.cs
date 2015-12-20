using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileManager
{
    public partial class FormMain : Form
    {
        FileManagerCore fmC1, fmC2;
        public FormMain()
        {
            InitializeComponent();

            fmC1 = new FileManagerCore(listBox1, textBoxAddress1, comboBox1);
            fmC2 = new FileManagerCore(listBox2, textBoxAddress2, comboBox2);
        }
        private void listBoxDoubleClick(object sender, MouseEventArgs e)
        {
            FileManagerCore fmC = null;
            ListBox listBox = sender as ListBox;
            if (listBox == listBox1)
                fmC = fmC1;
            if (listBox == listBox2)
                fmC = fmC2;

            if (listBox.SelectedItem != null)
            {
                try
                {
                    fmC.enter((FSItem)listBox.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void changeDriveInComboBox(object sender, EventArgs e)
        {
            FileManagerCore fmC = null;
            ComboBox comboBox = sender as ComboBox;
            if(comboBox == comboBox1)
                fmC = fmC1;
            if (comboBox == comboBox2)
                fmC = fmC2;

            if (fmC != null)
            {
                try
                {
                    fmC.changeDrive(comboBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            fmC1.goBack();
        }

        private void btnForward1_Click(object sender, EventArgs e)
        {
            fmC1.goForward();
        }

        private void btnUp1_Click(object sender, EventArgs e)
        {
            fmC1.goUp();
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            fmC2.goBack();
        }

        private void btnForward2_Click(object sender, EventArgs e)
        {
            fmC2.goForward();
        }

        private void btnUp2_Click(object sender, EventArgs e)
        {
            fmC2.goUp();
        }
        private ListBox getListBoxByCursorPos()
        {
            if (listBox1.PointToClient(Cursor.Position).X >= 0 && listBox1.PointToClient(Cursor.Position).X <= listBox1.Size.Width)
                return listBox1;
            if (listBox2.PointToClient(Cursor.Position).X >= 0 && listBox2.PointToClient(Cursor.Position).X <= listBox2.Size.Width)
                return listBox2;
            return null;
        }

        private void contextBtnDelete_Click(object sender, EventArgs e)
        {
            var listBox = getListBoxByCursorPos();
            Form wait = new FormWait("Идет удаление. Пожалуйста, подождите...");
            try
            {
                wait.Show();
                if (listBox == listBox1)
                    fmC1.delete((FSItem)listBox.SelectedItem);
                if (listBox == listBox2)
                    fmC2.delete((FSItem)listBox.SelectedItem);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Недостаточно прав. Запустите программу от имени администратора и убедитесь в том, что файл не имеет метки \"Только для чтения\".", "Ошибка прав доступа");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
            finally
            {
                wait.Close();
            }
        }

        private void listBoxMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var index = (sender as ListBox).IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                (sender as ListBox).SelectedIndex = index;
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void contextBtnPaste_Click(object sender, EventArgs e)
        {
            Form wait = new FormWait("Идет копирование. Пожалуйста, подождите...");
            try
            {
                wait.Show();
            var listBox = getListBoxByCursorPos();
                if (listBox == listBox1 && fmC2.getBuffer != null)
                {
                    fmC1.paste(fmC2.getBuffer);
                    if (fmC2.getCutFlag)
                        fmC2.delete(fmC2.getBuffer);
                }
                if (listBox == listBox2 && fmC1.getBuffer != null)
                {
                    fmC2.paste(fmC1.getBuffer);
                    if (fmC1.getCutFlag)
                        fmC1.delete(fmC1.getBuffer);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Недостаточно прав. Запустите программу от имени администратора и убедитесь в том, что файл не имеет метки \"Только для чтения\".", "Ошибка прав доступа");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                wait.Close();
            }
        }

        private void contextBtnCopy_Click(object sender, EventArgs e)
        {
            var listBox = getListBoxByCursorPos();
            try
            {
            if (listBox == listBox1)
                fmC1.copy((FSItem)listBox.SelectedItem);
            if (listBox == listBox2)
                fmC2.copy((FSItem)listBox.SelectedItem);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void contextBtnCut_Click(object sender, EventArgs e)
        {
            var listBox = getListBoxByCursorPos();
            if (listBox == listBox1)
                fmC1.cut((FSItem)listBox.SelectedItem);
            if (listBox == listBox2)
                fmC2.cut((FSItem)listBox.SelectedItem);
        }

        private void contextBtnSize_Click(object sender, EventArgs e)
        {
            var listBox = getListBoxByCursorPos();
            long size = -1; ;
            if (listBox == listBox1)
                size = fmC1.size((FSItem)listBox.SelectedItem);
            if (listBox == listBox2)
                size = fmC2.size((FSItem)listBox.SelectedItem);
            MessageBox.Show(size.ToString());
        }
    }
}
