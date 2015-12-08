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
        private void listBoxSelect()
        {
            ListBox listBox = null;
            FileManagerCore fmC = null;
            if (listBox1.Focused)
            {
                listBox = listBox1;
                fmC = fmC1;
            }
            if (listBox2.Focused)
            {
                listBox = listBox2;
                fmC = fmC2;
            }
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

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBoxSelect();
        }
        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listBoxSelect();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeDrive();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeDrive();
        }
        private void changeDrive()
        {
            FileManagerCore fmC = null;
            ComboBox comboBox = null;
            if (comboBox1.Focused)
            {
                fmC = fmC1;
                comboBox = comboBox1;
            }
            if (comboBox2.Focused)
            {
                fmC = fmC2;
                comboBox = comboBox2;
            }
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
            /*else
                MessageBox.Show("fmC1 is null");*/
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
