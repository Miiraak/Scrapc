namespace Scrapc
{
    internal class TextTraitement
    {
        internal static string GetFileNameFromUrl(string url)
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

        internal static void RemoveEmptyLines(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));

            File.WriteAllLines(filePath, nonEmptyLines);
        }
    }
}
