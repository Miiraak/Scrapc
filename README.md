# CScrap
## Introduction
CScrap est une application Windows Forms en C# permettant de crawler des sites web, d'extraire des URLs et de scraper le contenu de ces pages. Elle dispose d'une interface utilisateur pour gérer et visualiser les URLs collectées et le contenu extrait.
Fonctionnalités

- Crawling de site web : Collecte les URLs à partir d'une page donnée.
- Scraping de contenu : Extrait et sauvegarde le contenu des pages trouvées.
- Gestion des URLs : Affiche et sauvegarde les URLs collectées.

## Prérequis
- .NET Framework
- [HtmlAgilityPack](https://github.com/zzzprojects/html-agility-pack)

## Installation

### Clonez le dépôt :

#### sh

    git clone <URL_DU_DEPOT>

#### Visual Studio

    Ouvrez le projet avec Visual Studio.
    Ajoutez les dépendances nécessaires (HtmlAgilityPack).

## Utilisation

- Lancez l'application.
- Selectionnez le langage approprié.
- Entrez une URL valide dans le champ texte.
- Cliquez sur le bouton Crawl pour commencer le crawling.
- Utilisez Show URLs Gathered pour afficher les URLs récupérées. (optionnel)
- Cliquez sur Scrap pour extraire et sauvegarder le contenu des pages collectées.

## Fonctions
| Nom | État |
|:---:|:---:|
| URL Crawler | [🟢] |
| Scraper HTML | [🟢] |
| Multi-threading task | [🟠] |
| XML functions | [🛑] |
| ... | [⚫] |

## Contribution
Les contributions sont les bienvenues ! Veuillez ouvrir une issue ou soumettre une pull request pour toute amélioration ou correction de bugs.

## Licence 
Ce projet est sous licence MIT. Voir le fichier [LICENSE](https://github.com/Miiraak/CScrap/blob/master/LICENSE.txt) pour plus de détails.

# Disclaimer

Attention : L'utilisation de cette application doit se faire de manière responsable et légale.

- Respect des Conditions d'Utilisation : Assurez-vous de respecter les conditions d'utilisation des sites web que vous crawlez. De nombreux sites web interdisent explicitement le scraping ou limitent la fréquence des requêtes.
- DDoS (Distributed Denial of Service) : L'utilisation incorrecte de cette application peut entraîner un grand nombre de requêtes simultanées, potentiellement provoquant un DDoS involontaire. Limitez le nombre de requêtes simultanées et la fréquence des requêtes pour éviter cela.
- Contenu Interdit : Ne crawlez pas des sites web contenant des contenus illégaux ou des informations sensibles.

L'auteur de ce logiciel n'est pas responsable des dommages ou des conséquences juridiques résultant d'une utilisation inappropriée ou illégale de cette application.

---

Merci d'utiliser CScrap ! N'hésitez pas à faire des suggestions ou à signaler des bugs.
