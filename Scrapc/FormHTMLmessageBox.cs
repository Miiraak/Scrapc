﻿namespace Scrapc
{
    public partial class FormHTMLMessageBox : Form
    {
        public FormHTMLMessageBox()
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
