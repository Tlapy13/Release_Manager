using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Release_Manager
{
    public partial class CustomDialog : Form
    {
        public string MessageText { 
            get => this.messageTextBox.Text;
            set
            {
                this.messageTextBox.Text = value;
            }
        }

        public CustomDialog()
        {
            InitializeComponent();
        }
    }
}
