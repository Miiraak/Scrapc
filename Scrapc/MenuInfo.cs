namespace Scrapc
{
    public partial class MenuInfo : Form
    {
        internal HttpClient httpClient = new();
        internal List<string> visitedUrls = [];
        internal SemaphoreSlim semaphore = new(10);

        public static List<string> AllUrls { get; set; } = [];

        public MenuInfo()
        {
            InitializeComponent();
            ButtonScrap.Enabled = false;
            ButtonShowUrlsGathered.Enabled = false;
        }

        /// <summary>
        /// Used to go back to the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Used to crawl the website. Verify if the URL is valid and start the crawling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonCrawl_Click(object sender, EventArgs e)
        {
            if (Uri.IsWellFormedUriString(textBoxURL.Text, UriKind.Absolute))
            {
                ButtonCrawl.Enabled = false;

                visitedUrls.Clear();
                AllUrls.Clear();

                // Crawl the website
                await Crawling.CrawlWebsite(textBoxURL.Text, textBoxNumberCrawl, AllUrls, visitedUrls, httpClient);

                System.Windows.Forms.MessageBox.Show($"Crawling completed!\nCount : {AllUrls.Count}");

                ButtonScrap.Enabled = true;
                ButtonCrawl.Enabled = true;
                ButtonShowUrlsGathered.Enabled = true;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please enter a valid URL.");
            }
        }

        /// <summary>
        /// Used to scrape the website.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonScrap_Click(object sender, EventArgs e)
        {
            ButtonScrap.Enabled = false;
            ButtonCrawl.Enabled = false;
            ButtonShowUrlsGathered.Enabled = false;

            await Scraping.ScrapeWebSite(folderBrowserDialogHTML, AllUrls, textBoxURL, semaphore, httpClient, Menu.FonctionIndex);

            ButtonScrap.Enabled = true;
            ButtonCrawl.Enabled = true;
            ButtonShowUrlsGathered.Enabled = true;
        }

        /// <summary>
        /// Used to show the URLs gathered. Open a new form to display the URLs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonShowUrlsGathered_Click(object sender, EventArgs e)
        {
            MessageBox formHTMLMessageBox = new();
            formHTMLMessageBox.Show();
        }
    }
}
