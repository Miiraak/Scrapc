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
            foreach (var url in MenuInfo.AllUrls)
            {
                checkedListBoxURL.Items.Add(url);
            }

            for (int i = 0; i < checkedListBoxURL.Items.Count; i++)
            {
                checkedListBoxURL.SetItemChecked(i, true);
            }
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            MenuInfo.AllUrls.Clear();
            MenuInfo.AllUrls = checkedListBoxURL.CheckedItems.Cast<string>().ToList();
            this.Close();
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxURL.Items.Count; i++)
            {
                checkedListBoxURL.SetItemChecked(i, true);
            }
        }

        private void buttonUnselect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBoxURL.Items.Count; i++)
            {
                checkedListBoxURL.SetItemChecked(i, false);
            }
        }
    }
}
