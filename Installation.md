# Installation du poste développeur

## Applications et langages requis

_Remarque : vous pouvez installer la plupart de ces outils avec `winget` au lieu de suivre les installateurs des liens ci-dessous._

- [Visual Studio 2026](https://visualstudio.microsoft.com/fr/downloads/)
- [Visual Studio Code](https://code.visualstudio.com/Download)
- [NodeJS](https://nodejs.org/en/download/) (n'importe quelle version supportée)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli)
- [Terraform](https://www.terraform.io/downloads)
- [Docker](https://docs.docker.com/engine/install/ubuntu/#install-using-the-repository) (instructions pour Ubuntu, dans WSL 2 a priori)
- [jq](https://jqlang.github.io/jq/download/), pour le script de configuration locale

Les outils suivants devraient déjà être installés avec les outils précédents :

- [SDK .NET](https://dotnet.microsoft.com/en-us/download)
- [Git](https://git-scm.com/download/win)

## Extensions à utiliser

- VS Code : TopModel, Oxc
- Visual Studio : Csharpier

### Console bash par défaut dans VS Code

1. Ouvrez VS Code.
2. Ouvrez File => Preferences => Settings.
3. Dans Terminal › Integrated › Default Profile: Windows, choisissez "Git Bash".

## Comment lancer l’application

Après avoir déployé l'environnement `dev` avec Terraform, lancez `./get-env.sh` depuis le dossier `infra` pour renseigner la configuration Entra ID et stockage utilisée en local (voir [la mise en route](./sources/readme.md)).

- Exécutez `docker compose up -d` depuis le dossier `sources` pour lancer PostgreSQL.
- Exécutez `dotnet run` depuis le dossier `migrations` pour initialiser ou mettre à jour la base locale.
- Exécutez `dotnet run` depuis le dossier `sources/back/KleeContrib.Dfta.Api` pour lancer le back-end sur le port 2468 (ou lancez-le depuis Visual Studio).
- Exécutez `npm ci`, puis `npm start` depuis le dossier `sources/front` pour lancer le front-end sur le port 1357.
