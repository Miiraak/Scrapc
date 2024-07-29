﻿using System.Data;

namespace Scrapc
{
    public partial class FormHTML : Form
    {
        internal HttpClient httpClient;
        internal HashSet<string> visitedUrls;
        internal SemaphoreSlim semaphore;

        public static List<string> AllUrls { get; set; } = [];

        public FormHTML()
        {
            InitializeComponent();
            ButtonScrap.Enabled = false;
            ButtonShowUrlsGathered.Enabled = false;

            httpClient = new();
            visitedUrls = [];

            semaphore = new SemaphoreSlim(10);
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void ButtonCrawl_Click(object sender, EventArgs e)
        {
            if (Uri.IsWellFormedUriString(textBoxURL.Text, UriKind.Absolute))
            {
                ButtonCrawl.Enabled = false;

                visitedUrls.Clear();
                AllUrls.Clear();

                await CrawlWebsite(textBoxURL.Text);

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
        private async Task CrawlWebsite(string url)
        {
            if (!visitedUrls.Contains(url) && AllUrls.Count < Convert.ToInt32(textBoxNumberCrawl.Text))
            {
                visitedUrls.Add(url);
                AllUrls.Add(url);
                try
                {
                    string htmlContent = await httpClient.GetStringAsync(url);
                    List<string> urlsOnPage = ExtractUrls(htmlContent, url);

                    var crawlTasks = urlsOnPage.Select(foundUrl => CrawlWebsite(foundUrl));
                    await Task.WhenAll(crawlTasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error crawling {url}: {ex}");
                }
            }
            else
            {
                Console.WriteLine("Already visited or too many URLs");
            }
        }

        private List<string> ExtractUrls(string htmlContent, string baseUrl)
        {
            var urls = new List<string>();
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            var linkNodes = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
            if (linkNodes != null)
            {
                foreach (var linkNode in linkNodes)
                {
                    var hrefValue = linkNode.GetAttributeValue("href", string.Empty);
                    if (Uri.IsWellFormedUriString(hrefValue, UriKind.Relative))
                    {
                        hrefValue = new Uri(new Uri(baseUrl), hrefValue).ToString();
                    }

                    if (Uri.IsWellFormedUriString(hrefValue, UriKind.Absolute))
                    {
                        var uri = new Uri(hrefValue);
                        if ((uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps) && !visitedUrls.Contains(hrefValue))
                        {
                            if (uri.Host == new Uri(baseUrl).Host)
                            {
                                urls.Add(hrefValue);
                            }
                        }
                    }
                }
            }
            return urls;
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
                    var urlsToScrape = new List<string>(AllUrls);

                    DirectoryInfo dir = Directory.CreateDirectory(Path.Combine(folderBrowserDialogHTML.SelectedPath, $"{GetFileNameFromUrl(textBoxURL.Text)}{DateTime.Now:[yyyy-MM-dd_HH-mm-ss]}"));

                    string filePathCheck = Path.Combine(dir.FullName, "All_Urls_Gathered.txt");
                    string StringAllUrls = string.Join("\n", AllUrls);
                    File.WriteAllText(filePathCheck, StringAllUrls);

                    var scrapeTasks = urlsToScrape.Select(url => ScrapeAndSave(url, dir));

                    await Task.WhenAll(scrapeTasks);

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
                string filePath = Path.Combine(dir.FullName, $"{fileName}.html");

                string html = await FetchUrlAsync(url);
                HtmlAgilityPack.HtmlDocument doc = new();
                doc.LoadHtml(html);

                if (doc != null)
                {
                    File.WriteAllText(filePath, doc.Text.ToString());

                    MessageBox.Show($"Fichier enregistré avec succès à {filePath}");
                }
                else
                {
                    MessageBox.Show("Aucun contenu trouvé");
                }
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine($"Error scraping {url}: Host not found : {ex}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error scraping {url}: {ex}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error scraping {url}: {ex}");
            }
            finally
            {
                semaphore.Release();
            }
        }

        private async Task<string> FetchUrlAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching {url}: {ex}");
                throw;
            }
            finally
            {
                await Task.Delay(1000);
            }
        }

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        private static string GetFileNameFromUrl(string url)
        {
            if (url.Length > 100)
            {
                url = url.Remove(100, url.Length);
            }
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
            FormHTMLMessageBox formHTMLMessageBox = new();
            formHTMLMessageBox.Show();
        }
    }                                        
}
