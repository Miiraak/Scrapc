namespace Scrapc
{
    /// <summary>
    /// Represents the crawling of a website
    /// </summary>
    internal class Crawling
    {
        /// <summary>
        /// Crawl the website.
        /// </summary>
        /// <param name="URL">The URL where crawl should start.</param>
        /// <param name="textBoxNumberCrawl">The maximum of URLs to crawl.</param>
        /// <param name="AllUrls">The list of all the URLs to scrap from the website.</param>
        /// <param name="visitedUrls">The list of all the URLs already visited.</param>
        /// <param name="httpClient">The httpClient used for the crawl.</param>
        /// <returns></returns>
        internal static async Task CrawlWebsite(string URL, TextBox textBoxNumberCrawl, List<string> AllUrls, List<string> visitedUrls, HttpClient httpClient)
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

        /// <summary>
        /// Extract URLs from the HTML content. base URL is .
        /// </summary>
        /// <param name="htmlContent">The HTML content of the page.</param>
        /// <param name="baseUrl">Used to resolve relative URLs</param>
        /// <param name="visitedUrls">The list of all the URLs already visited.</param>
        /// <returns>a list of URLs.</returns>
        internal static List<string> ExtractUrls(string htmlContent, string baseUrl, List<string> visitedUrls)
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
    }
}
