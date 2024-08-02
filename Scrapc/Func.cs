namespace Scrapc
{
    public class Func
    {


        // -------------------------------------------------------------------------------------
        // ------------------------------------ Crawling ---------------------------------------
        // -------------------------------------------------------------------------------------

        public async Task CrawlWebsite(string URL, TextBox textBoxNumberCrawl, List<string> AllUrls, List<string> visitedUrls, HttpClient httpClient)
        {
            if (!visitedUrls.Contains(URL) && AllUrls.Count < Convert.ToInt32(textBoxNumberCrawl.Text))
            {
                visitedUrls.Add(URL);
                AllUrls.Add(URL);
                try
                {
                    string htmlContent = await httpClient.GetStringAsync(URL);
                    List<string> urlsOnPage = ExtractUrls(htmlContent, URL, visitedUrls);

                    var crawlTasks = urlsOnPage.Select(foundUrl => CrawlWebsite(foundUrl, textBoxNumberCrawl, AllUrls, visitedUrls, httpClient));
                    await Task.WhenAll(crawlTasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error crawling : {ex}");
                }
            }
            else
            {
                Console.WriteLine("Already visited or too many URLs");
            }
        }

        private List<string> ExtractUrls(string htmlContent, string baseUrl, List<string> visitedUrls)
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

        // -------------------------------------------------------------------------------------
        // ------------------------------------ Scraping ---------------------------------------
        // -------------------------------------------------------------------------------------

        public async Task ScrapeWebSite(FolderBrowserDialog folderBrowserDialogXXX, List<string> AllUrls, TextBox textBoxURL, SemaphoreSlim semaphore, HttpClient httpClient, string fonction)
        {
            try
            {
                if (folderBrowserDialogXXX.ShowDialog() == DialogResult.OK)
                {
                    var urlsToScrape = new List<string>(AllUrls);

                    DirectoryInfo dir = Directory.CreateDirectory(Path.Combine(folderBrowserDialogXXX.SelectedPath, $"{GetFileNameFromUrl(textBoxURL.Text)}{DateTime.Now:[yyyy-MM-dd_HH-mm-ss]}"));

                    string filePathCheck = Path.Combine(dir.FullName, "All_Urls_Gathered.txt");
                    string StringAllUrls = string.Join("\n", AllUrls);
                    File.WriteAllText(filePathCheck, StringAllUrls);

                    var scrapeTasks = urlsToScrape.Select(url => ScrapeAndSave(url, dir, semaphore, httpClient, fonction, AllUrls));

                    await Task.WhenAll(scrapeTasks);

                    MessageBox.Show($"Scraping completed!\nFiles saved at {dir.FullName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une autre erreur s'est produite : {ex}");
            }
        }

        private async Task ScrapeAndSave(string url, DirectoryInfo dir, SemaphoreSlim semaphore, HttpClient httpClient, string fonction, List<string> AllUrls)
        {
            await semaphore.WaitAsync();
            try
            {
                string fileName = GetFileNameFromUrl(url);
                string filePath = "";
                switch (fonction)
                {
                    case "TEXT":
                        filePath = Path.Combine(dir.FullName, $"{fileName}.txt");
                        break;

                    case "HTML":
                        filePath = Path.Combine(dir.FullName, $"{fileName}.html");
                        break;

                    case "URL":
                        filePath = Path.Combine(dir.FullName, $"{fileName}.url");
                        break;

                    case "IMAGE":
                        filePath = Path.Combine(dir.FullName, $"{fileName}.jpg");
                        break;
                }

                string html = await FetchUrlAsync(url, httpClient);
                HtmlAgilityPack.HtmlDocument doc = new();
                doc.LoadHtml(html);

                switch (fonction)
                {
                    case "TEXT":
                        var bodyNode = doc.DocumentNode.SelectSingleNode("//body");
                        if (bodyNode != null)
                        {
                            File.WriteAllText(filePath, bodyNode.InnerText);

                            RemoveEmptyLines(filePath);

                            MessageBox.Show($"Fichier enregistré avec succès à {filePath}");
                        }
                        else
                        {
                            MessageBox.Show("Aucun contenu trouvé");
                        }
                        break;

                    case "HTML":
                        if (doc != null)
                        {
                            File.WriteAllText(filePath, doc.Text.ToString());

                            MessageBox.Show($"Fichier enregistré avec succès à {filePath}");
                        }
                        else
                        {
                            MessageBox.Show("Aucun contenu trouvé");
                        }
                        break;

                    case "IMAGE":
                        var imgNodes = doc.DocumentNode.SelectNodes("//img[@src]");
                        if (imgNodes != null)
                        {
                            AllUrls.Clear();
                            foreach (var imgNode in imgNodes)
                            {
                                var src = imgNode.GetAttributeValue("src", string.Empty);

                                if (Uri.IsWellFormedUriString(src, UriKind.Relative))
                                {
                                    src = new Uri(new Uri(url), src).ToString();
                                }

                                if (Uri.IsWellFormedUriString(src, UriKind.Absolute))
                                {
                                    if (!AllUrls.Contains(src))
                                    {
                                        AllUrls.Add(src);
                                    }
                                }
                            }
                        }
                        foreach (string urlX in AllUrls)
                        { 
                            try
                            {
                                var bytes = await httpClient.GetByteArrayAsync(urlX);
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

        private async Task<string> FetchUrlAsync(string url, HttpClient httpClient)
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

        // -------------------------------------------------------------------------------------
        // --------------------------------- Text traitment ------------------------------------
        // -------------------------------------------------------------------------------------

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

        private static void RemoveEmptyLines(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));

            File.WriteAllLines(filePath, nonEmptyLines);
        }
    }
}
