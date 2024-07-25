﻿using System.Data;

namespace Scrapc
{
    public partial class FormHTML : Form
    {
        internal HttpClient httpClient;
        internal HashSet<string> visitedUrls;
        internal SemaphoreSlim semaphore;

        // Liste contenant les urls récupérées
        public static List<string> AllUrls { get; set; } = [];

        public FormHTML()
        {
            InitializeComponent();
            ButtonScrap.Enabled = false;
            ButtonShowUrlsGathered.Enabled = false;

            // Instanciation 
            httpClient = new();
            visitedUrls = [];

            // Limitation des reqêtes smiultanées
            semaphore = new SemaphoreSlim(10);
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void ButtonCrawl_Click(object sender, EventArgs e)
        {
            // Vérifie si l'URL entré est correcte et absolue
            if (Uri.IsWellFormedUriString(textBoxURL.Text, UriKind.Absolute))
            {
                ButtonCrawl.Enabled = false;

                // Nettoie les listes de urls
                visitedUrls.Clear();
                AllUrls.Clear();

                // Appele notre fonction et attend une réponse de CrawlWebSite
                await CrawlWebsite(textBoxURL.Text);

                MessageBox.Show($"Crawling completed!\nCount : {AllUrls.Count}");

                ButtonScrap.Enabled = true;
                ButtonCrawl.Enabled = true;
                ButtonShowUrlsGathered.Enabled = true;
            }
            else
            {
                Console.WriteLine("Please enter a valid URL.");
            }
        }
        private async Task CrawlWebsite(string url)
        {
            // Check si l'url est déja vérifiée et si le nombre récupérer ne dépasse pas la demande de l'utilisateur.
            if (!visitedUrls.Contains(url) && AllUrls.Count < Convert.ToInt32(textBoxNumberCrawl.Text))
            {
                // Ajoute notre url à celles visitées et à celles des urls à scraper
                visitedUrls.Add(url);
                AllUrls.Add(url);
                try
                {
                    // Effectue une requète asycrone pour récupérer le corps de la page.
                    string htmlContent = await httpClient.GetStringAsync(url);
                    // Extrait les URl contenus dans le corps de la page et les stocks
                    List<string> urlsOnPage = ExtractUrls(htmlContent, url);

                    // Création d'un tableau de tâches pour chaque URL trouvée
                    var crawlTasks = urlsOnPage.Select(foundUrl => CrawlWebsite(foundUrl));
                    // Attendre que toutes les tâches soient terminées
                    await Task.WhenAll(crawlTasks);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error crawling {url}: {ex.Message}");
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

            // Récupération des balises <a> contenant un attribut href
            var linkNodes = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
            if (linkNodes != null)
            {
                foreach (var linkNode in linkNodes)
                {
                    // Récupération de la valeur de l'attribut href
                    var hrefValue = linkNode.GetAttributeValue("href", string.Empty);
                    // Vérifier si l'URL est relative et la convertir en absolue
                    if (Uri.IsWellFormedUriString(hrefValue, UriKind.Relative))
                    {
                        hrefValue = new Uri(new Uri(baseUrl), hrefValue).ToString();
                    }

                    // Vérifier si l'URL est bien formée et est HTTP/HTTPS et si elle n'a pas déjà été visitée
                    if (Uri.IsWellFormedUriString(hrefValue, UriKind.Absolute))
                    {
                        var uri = new Uri(hrefValue);
                        if ((uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps) && !visitedUrls.Contains(hrefValue))
                        {
                            // Vérifier si l'URL appartient au même domaine que l'URL de base
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

                    // Création du dossier pour stocker les fichiers
                    DirectoryInfo dir = Directory.CreateDirectory(Path.Combine(folderBrowserDialogHTML.SelectedPath, $"{GetFileNameFromUrl(textBoxURL.Text)}{DateTime.Now:[yyyy-MM-dd_HH-mm-ss]}"));

                    // Création du fichier contenant toute les urls utilisée pour le scraping
                    string filePathCheck = Path.Combine(dir.FullName, "All_Urls_Gathered.txt");
                    string StringAllUrls = string.Join("\n", AllUrls);
                    File.WriteAllText(filePathCheck, StringAllUrls);

                    // Création d'un tableau de tâches pour le scraping
                    var scrapeTasks = urlsToScrape.Select(url => ScrapeAndSave(url, dir));

                    // Attendre que toutes les tâches soient terminées
                    await Task.WhenAll(scrapeTasks);

                    MessageBox.Show($"Scraping completed!\nFiles saved at {dir.FullName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une autre erreur s'est produite : {ex.Message}");
            }
        }

        private async Task ScrapeAndSave(string url, DirectoryInfo dir)
        {
            await semaphore.WaitAsync();
            try
            {
                string fileName = GetFileNameFromUrl(url);
                string filePath = Path.Combine(dir.FullName, $"{fileName}.txt");

                string html = await FetchUrlAsync(url);
                HtmlAgilityPack.HtmlDocument doc = new();
                doc.LoadHtml(html);

                if (doc != null)
                {
                    File.WriteAllText(filePath, doc.Text.ToString());

                    Console.WriteLine($"Fichier enregistré avec succès à {filePath}");
                }
                else
                {
                    Console.WriteLine("Aucun contenu trouvé");
                }
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine($"Error scraping {url}: Host not found : {ex.Message}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error scraping {url}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error scraping {url}: {ex.Message}");
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
                Console.WriteLine($"Error fetching {url}: {ex.Message}");
                throw;
            }
            finally
            {
                await Task.Delay(1000);
            }
        }

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
