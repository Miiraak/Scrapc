# Scrapc
## Introduction
Scrapc est une application Windows Forms en C# permettant de crawler des sites web, d'extraire des URLs et de scraper le contenu de ces pages. Elle dispose d'une interface utilisateur pour gérer et visualiser les URLs collectées et le contenu extrait.

## Fonctionnalités

- Crawling de site web : Collecte les URLs à partir d'une page donnée.
- Scraping de contenu : Extrait et sauvegarde le contenu des pages trouvées.
- Scraping HTML : Extrait et sauvegarde le code HTML des pages trouvées.
- Scraping d'URLs : Affiche et sauvegarde les URLs collectées.

## Prérequis
- .NET Framework
- [HtmlAgilityPack](https://github.com/zzzprojects/html-agility-pack)

## Installation

### Clonez le dépôt :

#### sh

    git clone <URL_DU_DEPOT>

#### Visual Studio

    Ouvrez le projet avec Visual Studio.
    Ajoutez les dépendances nécessaires (HtmlAgilityPack) via NuGet.

## Utilisation

- Lancez l'application.
- Selectionnez la fonction approprié.
<img src="/Images/Menu.png" width="300" height="140">
- Entrez une URL valide dans le champ texte.
- Choississez le nombre d'url que vous souhaitez explorer au maximum.
- Cliquez sur le bouton `Crawl` pour commencer le crawling.
<img src="/Images/MenuHTML.png" width="300" height="200">
- Utilisez `URLs ?` pour afficher les URLs récupérées. (optionnel)
<img src="/Images/ShowURLs.png" width="441" height="442">
- Cliquez sur `Scrap` pour extraire et sauvegarder le contenu des pages collectées selon la fonction choisie et les URLs utilisés. <br>

## Fonctions
| Nom | État |
|:---:|:---:|
| Site Crawler | [🟢] |
| Text Scraper  | [🟢] |
| HTML Scraper  | [🟢] |
| URL Scraper| [🟢] |
| Multi-threading task | [🟢] |
| Images functions | [🛑] |
| ... | [⚫] |

## Contribution
Les contributions sont les bienvenues ! Veuillez ouvrir une issue ou soumettre une pull request pour toute amélioration ou correction de bugs.

## Licence 
Ce projet est sous licence MIT. Voir le fichier [LICENSE](https://github.com/Miiraak/Scrapc/blob/master/LICENSE.txt) pour plus de détails.

# Disclaimer

Attention : L'utilisation de cette application doit se faire de manière responsable et légale.

- Respect des Conditions d'Utilisation : Assurez-vous de respecter les conditions d'utilisation des sites web que vous crawlez. De nombreux sites web interdisent explicitement le scraping ou limitent la fréquence des requêtes.
- DDoS (Distributed Denial of Service) : L'utilisation incorrecte de cette application peut entraîner un grand nombre de requêtes simultanées, potentiellement provoquant un DDoS involontaire. Limitez le nombre de requêtes simultanées et la fréquence des requêtes pour éviter cela.
- Contenu Interdit : Ne crawlez pas des sites web contenant des contenus illégaux ou des informations sensibles.

L'auteur de ce logiciel n'est pas responsable des dommages ou des conséquences juridiques résultant d'une utilisation inappropriée ou illégale de cette application.

---

### Sinon merci d'utiliser Scrapc ! N'hésitez pas à faire des suggestions ou à signaler des bugs.

---
