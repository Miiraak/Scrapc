namespace Scrapc
{
    public partial class FormImageMessageBox : Form
    {
        public FormImageMessageBox()
        {
            InitializeComponent();
            ShowURLs();
        }

        private void ShowURLs()
        {
            richTextBoxListURL.Text = string.Join("\n", FormImage.AllUrls);
        }
    }
}
