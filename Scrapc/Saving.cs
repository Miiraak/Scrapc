namespace Scrapc
{
    internal class Saving
    {
        /// <summary>
        /// Used to save the text content of the website.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="filePath"></param>
        internal static void SaveText(HtmlAgilityPack.HtmlDocument doc, string filePath)
        {
            var bodyNode = doc.DocumentNode.SelectSingleNode("//body");
            if (bodyNode != null)
            {
                File.WriteAllText(filePath, bodyNode.InnerText);

                TextTraitement.RemoveEmptyLines(filePath);

                System.Windows.Forms.MessageBox.Show($"Fichier enregistré avec succès à {filePath}");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Aucun contenu trouvé");
            }
        }

        /// <summary>
        /// Used to save the HTML content of the website.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="filePath"></param>
        internal static void SaveHtml(HtmlAgilityPack.HtmlDocument doc, string filePath)
        {
            if (doc != null)
            {
                File.WriteAllText(filePath, doc.Text.ToString());

                System.Windows.Forms.MessageBox.Show($"Fichier enregistré avec succès à {filePath}");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Aucun contenu trouvé");
            }
        }

        /// <summary>
        /// Used to save the image of the website.
        /// </summary>
        internal static void SaveImage()
        {

        }

        /// <summary>
        /// Used to save the URL of the website.
        /// </summary>
        internal static void SaveUrls()
        {

        }
    }
}
