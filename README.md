# Scrapc
## Description
A Windows Form app that let you recursivly crawl and crape web site to extract differents content.  

---

## Features
| Label | Desc |
|---|---|
| **Crawling** | Recursivly collect URLs from given web pages. | 
| **Scraping content** | Extract and save the web page content. |
| **Scraping HTLM** | Extract and save the HTML's web pages. |
| **Scraping Image** | Extract and save images from web pages. |
| **Scraping URLS** | Extract and save all urls encountered from web pages |
| **URLs limitation** | Choose the maximal number of urls to scrape. |

---

# Disclaimer

Warning: The use of this application must be done in a responsible and legal way.

- Compliance with the Terms of Use: Make sure you comply with the terms of use of the websites that you are crawling. Many websites limit the frequency of requests, explicitly prohibit scraping or access to certain resources. (Sorry Wikipedia it was not intended 😅🙏)
- Distributed Denial of Service (DDoS): Improper use of this application can result in a large number of simultaneous requests, potentially causing an unintended DDoS. Limit the number of simultaneous requests and the frequency of requests to avoid this.
- Prohibited Content: Do not crawl websites containing illegal content or sensitive information.

The author of this software is not responsible for any damages or legal consequences resulting from improper or illegal use of this application.

---

## Prerequisites
Before running the project, make sure you have the following installed:

- [.NET Framework](https://dotnet.microsoft.com/fr-fr/download/dotnet-framework)
- [HtmlAgilityPack](https://github.com/zzzprojects/html-agility-pack)

---

## Usage
- Start App.
- Select wanted content.
<img src="/Images/Menu.png" width="300" height="140">

- Enter a valid url in the right field.<br>
_You can try it with : [Book to Scrape](https://books.toscrape.com/) (Thanks to them 🫀)_
- Choose the maximum number of urls you wanna crawl.
- Click on `crawl` to start the gathering.
<img src="/Images/MenuHTML.png" width="300" height="200">

- Use `URLs ?` to show URLs gathered. (optionnal)
<img src="/Images/ShowURLs.png" width="441" height="442">

- Next click `Scrap` to extract and save the choosen content, defined on step 2, of the crawled pages.

---

## Contributing
Contributions are welcome! To contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature (`git checkout -b my-new-feature`).
3. Make your changes.
4. Commit your changes (`git commit -m 'Add my new feature'`).
5. Push your branch (`git push origin my-new-feature`).
6. Open a Pull Request.

---

## Issues and Suggestions
If you encounter any issues or have suggestions for improving the project, please use the [GitHub issue tracker](https://github.com/Miiraak/[APP_NAME]]/issues).

---

## License
This project is licensed under the MIT. See the [LICENSE](./LICENSE) file for more details.

---

## Authors
**[Miiraak](https://github.com/miiraak)** - *Lead Developer* - 

