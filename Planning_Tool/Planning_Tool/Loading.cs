using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Planning_Tool
{
    public partial class Loading : Form
    {
        public string xmlpath;

        public Loading(string infoString)
        {
            InitializeComponent();
            info.Text = infoString;
        }
    }
}
