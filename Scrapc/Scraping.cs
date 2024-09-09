namespace Scrapc
{
    /// <summary>
    /// This class contains the methods to scrape a website and save the content in a file.
    /// </summary>
    internal class Scraping
    {
        /// <summary>
        /// Scrape the website.
        /// </summary>
        /// <param name="folderBrowserDialogXXX">Used to select the folder where the files will be saved.</param>
        /// <param name="AllUrls">The list of all the URLs to scrap from the website.</param>
        /// <param name="textBoxURL">Used to get the URL to scrape.</param>
        /// <param name="semaphore">Used to limit the number of simultaneous requests.</param>
        /// <param name="httpClient">Used to fetch the content of the website.</param>
        /// <param name="fonction">Used to determine the type of file to save.</param>
        internal static async Task ScrapeWebSite(FolderBrowserDialog folderBrowserDialogXXX, List<string> AllUrls, TextBox textBoxURL, SemaphoreSlim semaphore, HttpClient httpClient, int fonction)
        {
            try
            {
                if (folderBrowserDialogXXX.ShowDialog() == DialogResult.OK)
                {
                    var urlsToScrape = new List<string>(AllUrls);

                    DirectoryInfo dir = Directory.CreateDirectory(Path.Combine(folderBrowserDialogXXX.SelectedPath, $"{TextTraitement.GetFileNameFromUrl(textBoxURL.Text)}{DateTime.Now:[yyyy-MM-dd_HH-mm-ss]}"));

                    string filePathCheck = Path.Combine(dir.FullName, "All_Urls_Gathered.txt");
                    string StringAllUrls = string.Join("\n", AllUrls);
                    File.WriteAllText(filePathCheck, StringAllUrls);

                    var scrapeTasks = urlsToScrape.Select(url => Save(url, dir, semaphore, httpClient, fonction));
                    await Task.WhenAll(scrapeTasks);

                    System.Windows.Forms.MessageBox.Show($"Scraping completed!\nFiles saved at {dir.FullName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une autre erreur s'est produite : {ex}");
            }
        }

        /// <summary>
        /// Save the content of the website in a file.
        /// </summary>
        /// <param name="url">The URL of the website to scrape.</param>
        /// <param name="dir">The directory where the files will be saved. </param>
        /// <param name="semaphore">Used to limit the number of simultaneous requests.</param>
        /// <param name="httpClient">Used to fetch the content of the website.</param>
        /// <param name="fonction">Used to determine the type of file to save.</param>
        internal static async Task Save(string url, DirectoryInfo dir, SemaphoreSlim semaphore, HttpClient httpClient, int fonction)
        {
            await semaphore.WaitAsync();
            try
            {
                string fileName = TextTraitement.GetFileNameFromUrl(url);
                string filePath = "";

                switch (fonction)
                {
                    case 0:
                        filePath = Path.Combine(dir.FullName, $"{fileName}.txt");
                        break;

                    case 1:
                        filePath = Path.Combine(dir.FullName, $"{fileName}.html");
                        break;

                    case 2:
                        filePath = Path.Combine(dir.FullName, $"{fileName}.url");
                        break;

                    case 3:
                        filePath = Path.Combine(dir.FullName, $"{fileName}.jpg");
                        break;
                }

                string html = await FetchUrlAsync(url, httpClient);
                HtmlAgilityPack.HtmlDocument doc = new();
                doc.LoadHtml(html);

                switch (fonction)
                {
                    case 0:
                        Saving.SaveText(doc, filePath);
                        break;

                    case 1:
                        Saving.SaveHtml(doc, filePath);
                        break;

                    case 2:
                        break;

                    case 3:
                        break;
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

        /// <summary>
        /// Fetch the content of the website. Wait 1 second before returning.    
        /// </summary>
        /// <param name="url"></param>
        /// <param name="httpClient"></param>
        /// <returns>Returns the content of the website as a string.</returns>
        internal static async Task<string> FetchUrlAsync(string url, HttpClient httpClient)
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
    }
}
