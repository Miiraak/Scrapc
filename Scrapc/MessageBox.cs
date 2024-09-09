namespace Scrapc
{
    public partial class MessageBox : Form
    {
        public MessageBox()
        {
            InitializeComponent();
            ShowURLs();
        }

        /// <summary>
        /// Show the URLs gathered
        /// </summary>
        private void ShowURLs()
        {
            // Display all the URLs gathered into the richTextBoxListURL
            richTextBoxListURL.Text = string.Join("\n", MenuInfo.AllUrls);
        }
    }
}
