namespace Scrapc
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
            // Display all the URLs gathered into the richTextBoxListURL
            richTextBoxListURL.Text = string.Join("\n", FormHTML.AllUrls);
        }
    }
}
