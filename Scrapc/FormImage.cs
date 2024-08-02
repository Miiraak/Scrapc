namespace Scrapc
{
    public partial class FormImage : Form
    {
        internal HttpClient httpClient;
        internal List<string> visitedUrls;
        internal SemaphoreSlim semaphore;
        internal Func func;

        public static List<string> AllUrls { get; set; } = [];

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        public FormImage()
        {
            InitializeComponent();
            ButtonScrap.Enabled = false;
            ButtonShowUrlsGathered.Enabled = false;

            httpClient = new();
            visitedUrls = new();
            func = new();

            semaphore = new SemaphoreSlim(5);
        }

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        private async void ButtonCrawl_Click(object sender, EventArgs e)
        {
            if (Uri.IsWellFormedUriString(textBoxURL.Text, UriKind.Absolute))
            {
                ButtonCrawl.Enabled = false;

                AllUrls.Clear();

                await func.CrawlWebsite(textBoxURL.Text, textBoxNumberCrawl, AllUrls, visitedUrls, httpClient);

                MessageBox.Show($"Crawling completed!\nCount : {AllUrls.Count}");

                ButtonScrap.Enabled = true;
                ButtonCrawl.Enabled = true;
                ButtonShowUrlsGathered.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid URL.");
            }
        }

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        private async void ButtonScrap_Click(object sender, EventArgs e)
        {
            ButtonScrap.Enabled = false;
            ButtonCrawl.Enabled = false;
            ButtonShowUrlsGathered.Enabled = false;

            await func.ScrapeWebSite(folderBrowserDialogIMAGE, AllUrls, textBoxURL, semaphore, httpClient, "IMAGE");

            ButtonScrap.Enabled = true;
            ButtonCrawl.Enabled = true;
            ButtonShowUrlsGathered.Enabled = true;
        }

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        private void ButtonShowUrlsGathered_Click(object sender, EventArgs e)
        {
            FormImageMessageBox formImageMessageBox = new();
            formImageMessageBox.Show();
        }
    }
}
