using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void toolStripButton2_Click(object sender, EventArgs e)
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

        
    }
}
