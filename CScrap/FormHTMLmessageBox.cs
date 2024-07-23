using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CScrap
{
    public partial class FormHTMLmessageBox : Form
    {
        public FormHTMLmessageBox()
        {
            InitializeComponent();
            ShowURLs();
        }

        private void ShowURLs()
        {
            richTextBoxListURL.Text = string.Join("\n", FormHTML.AllUrls);
        }
    }
}
