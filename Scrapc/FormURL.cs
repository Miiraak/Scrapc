using System.Data;

namespace Scrapc
{
    public partial class FormURL : Form
    {
        internal HttpClient httpClient;
        internal SemaphoreSlim semaphore;

        // Liste contenant les urls récupérées
        public static List<string> AllUrls { get; set; } = [];

        public FormURL()
        {
            InitializeComponent();
            ButtonScrap.Enabled = false;
            ButtonShowUrlsGathered.Enabled = false;

            // Instanciation 
            httpClient = new();

            // Limitation des reqêtes smiultanées
            semaphore = new SemaphoreSlim(10);
        }

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            // Ferme FormTEXT
            this.Close();
        }

        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------

        private async void ButtonCrawl_Click(object sender, EventArgs e)
        {
            // Vérifie si l'URL entré est correcte et absolue
            if (Uri.IsWellFormedUriString(textBoxURL.Text, UriKind.Absolute))
            {
                ButtonCrawl.Enabled = false;

                // Nettoie les listes de urls
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
                MessageBox.Show("Please enter a valid URL.");
            }
        }

        // Récupère les liens URL contenus dans la page entré dans le textBoxURL
        private async Task CrawlWebsite(string url)
        {
            // Check si l'url est déja vérifiée et si le nombre récupérer ne dépasse pas la demande de l'utilisateur.
            if (!AllUrls.Contains(url) && AllUrls.Count < Convert.ToInt32(textBoxNumberCrawl.Text))
            {
                // Ajoute notre url à celles visitées
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
                    Console.WriteLine($"Error crawling {url}: {ex}");
                }
            }
            else
            {
                Console.WriteLine("Already visited or too many URLs");
            }
        }

        // Extrait les URL de la page HTML
        private static List<string> ExtractUrls(string htmlContent, string baseUrl)
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

                    // Vérifier si l'URL est bien formée et est HTTP/HTTPS
                    if (Uri.IsWellFormedUriString(hrefValue, UriKind.Absolute))
                    {
                        // Création d'un objet Uri pour vérifier le domaine
                        var uri = new Uri(hrefValue);

                        // Vérifier si l'URL est bien formée et est HTTP/HTTPS et si elle n'a pas déjà été visitée
                        if ((uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps) && !AllUrls.Contains(hrefValue))
                        {
                            // Vérifier si l'URL appartient au même domaine que l'URL de base
                            if (uri.Host == new Uri(baseUrl).Host)
                            {
                                // Ajouter l'URL à la liste des URLs de la page
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
                if (folderBrowserDialogTEXT.ShowDialog() == DialogResult.OK)
                {
                    var urlsToScrape = new List<string>(AllUrls);

                    // Création du dossier pour stocker les fichiers
                    DirectoryInfo dir = Directory.CreateDirectory(Path.Combine(folderBrowserDialogTEXT.SelectedPath, $"{GetFileNameFromUrl(textBoxURL.Text)}{DateTime.Now:[yyyy-MM-dd_HH-mm-ss]}"));

                    // Création du fichier contenant toute les urls utilisée pour le scraping
                    string filePathCheck = Path.Combine(dir.FullName, "All_Urls_Gathered.txt");
                    string StringAllUrls = string.Join("\n", AllUrls);
                    File.WriteAllText(filePathCheck, StringAllUrls);

                    MessageBox.Show($"Scraping completed!\nFiles saved at {dir.FullName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une autre erreur s'est produite : {ex}");
            }
        }

        // Fonction pour créer le nom du fichier à partir de l'url
        private static string GetFileNameFromUrl(string url)
        {
            // Limite la longueur du nom du fichier => evite les erreurs dues à des fichiers/chemins absolus trop longs.
            if (url.Length > 100)
            {
                url = url.Remove(100, url.Length);
            }
            // Vérification du protocole utilisé
            if (url.StartsWith("http://"))
            {
                // Remplacement des caractères spéciaux par des caractères autorisés
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
                // Similaire à la condition précédente
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
                // Si le protocole est différent, on remplace les caractères spéciaux
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

        // Fonction pour afficher les urls récupérées, appel de la FormTEXTMessageBox
        private void ButtonShowUrlsGathered_Click(object sender, EventArgs e)
        {
            FormURLMessageBox formURLmessageBox = new();
            formURLmessageBox.Show();
        }
    }
}
