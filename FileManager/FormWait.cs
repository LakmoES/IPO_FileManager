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
    public partial class FormWait : Form
    {
        public FormWait(string text1)
        {
            InitializeComponent();
            this.label1.Text = text1;
        }
    }
}
