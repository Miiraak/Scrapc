namespace Scrapc
{
    public partial class FormHTML : Form
    {
        internal HttpClient httpClient;
        internal List<string> visitedUrls;
        internal SemaphoreSlim semaphore;
        internal Func func;

        // AllUrls is a list of all the URLs to scrap from the website
        public static List<string> AllUrls { get; set; } = [];

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        public FormHTML()
        {
            InitializeComponent();
            ButtonScrap.Enabled = false;
            ButtonShowUrlsGathered.Enabled = false;

            httpClient = new();
            visitedUrls = [];
            func = new();

            // Limit the number of concurrent requests to 10
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

                visitedUrls.Clear();
                AllUrls.Clear();

                // Crawl the website
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

            // Scrape the website
            await func.ScrapeWebSite(folderBrowserDialogHTML, AllUrls, textBoxURL, semaphore, httpClient, "HTML");

            ButtonScrap.Enabled = true;
            ButtonCrawl.Enabled = true;
            ButtonShowUrlsGathered.Enabled = true;
        }

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        // Show all the URLs gathered
        private void ButtonShowUrlsGathered_Click(object sender, EventArgs e)
        {
            FormHTMLMessageBox formHTMLMessageBox = new();
            formHTMLMessageBox.Show();
        }
    }                                        
}
