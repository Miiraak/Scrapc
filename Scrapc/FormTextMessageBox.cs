namespace Scrapc
{
    public partial class FormTextMessageBox : Form
    {
        public FormTextMessageBox()
        {
            InitializeComponent();
            ShowURLs();
        }

        private void ShowURLs()
        {
            richTextBoxListURL.Text = string.Join("\n", FormTEXT.AllUrls);
        }
    }
}
