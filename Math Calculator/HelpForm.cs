using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Math_Calculator
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent(); 
            textBox1.SelectionStart = 0;

        }
    }
}
