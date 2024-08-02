namespace Scrapc
{
    public partial class FormURL : Form
    {
        internal HttpClient httpClient;
        internal SemaphoreSlim semaphore;
        internal Func func;

        public static List<string> AllUrls { get; set; } = [];

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        public FormURL()
        {
            InitializeComponent();
            ButtonScrap.Enabled = false;
            ButtonShowUrlsGathered.Enabled = false;

            httpClient = new();
            func = new();

            semaphore = new SemaphoreSlim(10);
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

                // Unused variable, do no remove it, used to avoid errors in CrawlWebsite call.
                List<string> visitedUrls = new();
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

            await func.ScrapeWebSite(folderBrowserDialogURL, AllUrls, textBoxURL, semaphore, httpClient, "URL");

            ButtonScrap.Enabled = true;
            ButtonCrawl.Enabled = true;
            ButtonShowUrlsGathered.Enabled = true;
        }

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        private void ButtonShowUrlsGathered_Click(object sender, EventArgs e)
        {
            FormURLMessageBox formURLmessageBox = new();
            formURLmessageBox.Show();
        }
    }
}
