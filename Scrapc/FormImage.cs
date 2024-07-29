using System.Data;

namespace Scrapc
{
    public partial class FormImage : Form
    {
        internal HttpClient httpClient;
        internal SemaphoreSlim semaphore;

        // Liste contenant les urls des images récupérées
        public static List<string> ImageUrls { get; set; } = [];

        public FormImage()
        {
            InitializeComponent();
            ButtonScrap.Enabled = false;
            ButtonShowUrlsGathered.Enabled = false;

            httpClient = new();

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

                ImageUrls.Clear();

                await CrawlWebsite(textBoxURL.Text);

                MessageBox.Show($"Crawling completed!\nCount : {ImageUrls.Count}");

                ButtonScrap.Enabled = true;
                ButtonCrawl.Enabled = true;
                ButtonShowUrlsGathered.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid URL.");
            }
        }
        private async Task CrawlWebsite(string url)
        {
            try
            {
                string htmlContent = await httpClient.GetStringAsync(url);
                ExtractURLImage(htmlContent, url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crawling {url}: {ex}");
            }
        }

        private static void ExtractURLImage(string htmlContent, string baseUrl)
        {
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            // Selection des nodes contenant les sources images
            var imgNodes = htmlDocument.DocumentNode.SelectNodes("//img[@src]");
            if (imgNodes != null)
            {
                foreach (var imgNode in imgNodes)
                {
                    // Récupère la valeur de l'attribut src
                    var src = imgNode.GetAttributeValue("src", string.Empty);

                    // Formate l'url si elle est relative
                    if (Uri.IsWellFormedUriString(src, UriKind.Relative))
                    {
                        src = new Uri(new Uri(baseUrl), src).ToString();
                    }

                    if (Uri.IsWellFormedUriString(src, UriKind.Absolute))
                    {
                        // Vérifie si l'url est déjà dans la liste
                        if (!ImageUrls.Contains(src))
                        {
                            // Ajoute l'url à la liste
                            ImageUrls.Add(src);
                        }
                    }
                }
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

            await ScrapeWebSite();

            ButtonScrap.Enabled = true;
            ButtonCrawl.Enabled = true;
            ButtonShowUrlsGathered.Enabled = true;
        }

        private async Task ScrapeWebSite()
        {
            try
            {
                if (folderBrowserDialogHTML.ShowDialog() == DialogResult.OK)
                {
                    var urlsToScrape = new List<string>(ImageUrls);

                    DirectoryInfo dir = Directory.CreateDirectory(Path.Combine(folderBrowserDialogHTML.SelectedPath, $"{GetFileNameFromUrl(textBoxURL.Text)}{DateTime.Now:[yyyy-MM-dd_HH-mm-ss]}"));

                    string filePathCheck = Path.Combine(dir.FullName, "All_Urls_Gathered.txt");
                    string StringAllUrls = string.Join("\n", ImageUrls);
                    File.WriteAllText(filePathCheck, StringAllUrls);

                    // Création d'une liste de tâches pour télécharger les images, puis attendre qu'elles soient toutes terminées
                    var tasks = urlsToScrape.Select(url => Task.Run(() => ScrapeAndSave(url, dir)));
                    await Task.WhenAll(tasks);

                    MessageBox.Show($"Scraping completed!\nFiles saved at {dir.FullName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une autre erreur s'est produite : {ex}");
            }
        }

        private async Task ScrapeAndSave(string url, DirectoryInfo dir)
        {
            await semaphore.WaitAsync();
            try
            {
                string fileName = GetFileNameFromUrl(url);
                string filePath = Path.Combine(dir.FullName, fileName);

                var bytes = await httpClient.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(filePath, bytes);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du téléchargement de l'image {url}: {ex}");
            }
            finally
            {
                semaphore.Release();
            }
        }

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        private static string GetFileNameFromUrl(string url)
        {
            if (url.StartsWith("http://"))
            {
                return url.Remove(0, 10)
                          .Replace("/", "-")
                          .Replace("|", "_")
                          .Replace(@"\", "_")
                          .Replace('"', '_')
                          .Replace(":", "_")
                          .Replace("*", "_")
                          .Replace("?", "_")
                          .Replace("<", "_")
                          .Replace(">", "_");

            }
            else if (url.StartsWith("https://"))
            {
                return url.Remove(0, 11)
                          .Replace("/", "-")
                          .Replace("|", "_")
                          .Replace(@"\", "_")
                          .Replace('"', '_')
                          .Replace(":", "_")
                          .Replace("*", "_")
                          .Replace("?", "_")
                          .Replace("<", "_")
                          .Replace(">", "_");
            }
            else
            {
                return url.Replace(".", "_")
                          .Replace("/", "-")
                          .Replace("|", "_")
                          .Replace(@"\", "_")
                          .Replace('"', '_')
                          .Replace(":", "_")
                          .Replace("*", "_")
                          .Replace("?", "_")
                          .Replace("<", "_")
                          .Replace(">", "_");
            }
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
