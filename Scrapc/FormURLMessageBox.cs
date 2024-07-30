namespace Scrapc
{
    public partial class FormURLMessageBox : Form
    {
        public FormURLMessageBox()
        {
            InitializeComponent();
            ShowURLs();
        }

        private void ShowURLs()
        {
            richTextBoxListURL.Text = string.Join("\n", FormURL.AllUrls);
        }
    }
}
