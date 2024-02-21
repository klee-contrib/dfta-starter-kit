# Installation post développeur

## Application / Language requis

- [Visual Studio 2022 Pro (en incluant l'installation .NET 8)](https://visualstudio.microsoft.com/fr/downloads/)
- [Visual Studio Code](https://code.visualstudio.com/Download)
- [Installation de Terraform](https://www.terraform.io/downloads)
- [Installation d'AzureCli](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli-windows?tabs=azure-cli)
- [Installer puis ouvrir docker desktop](https://www.docker.com/products/docker-desktop/)
  > si besoin (un message à l'ouverture de docker le spécifiera), suivre le tuto
  > https://docs.microsoft.com/fr-fr/windows/wsl/install-manual#step-4---download-the-linux-kernel-update-package
- [Installer NodeJs (dernière version stable pour avoir npm)](https://nodejs.org/en/download/)
- [Installer Git](https://git-scm.com/download/win) (s'il n'est pas déjà installé)
- [Installer psql](https://www.postgresql.org/download/windows/)
  > Ajouter ensuite dans la variable d'environnement PATH de windows le chemin pour la commande psql
- [Installer Azure Data Studio Dev](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads?rtc=1)
- Installer l'extension PostgreSQL sur Azure Data Studio (vous pourrez ainsi vous connecter à la base)
- Installer TopModel, Prettier & Eslint en extension sur VS Code

**Redémarrer le pc**

## Console bash par défaut dans vs code

1. Ouvrir vs code
1. Ouvrir File => Preferences => Settings
1. Terminal › Integrated › Default Profile: Windows => Choisir "Git bash"

## Installation après récupération fichier source

1. Ouvrir le repo dans vs code & ouvrir un git bash puis exécuter
   ```sh
   cd sources/front
   npm install
   cd ..
   ```
1. Ouvrir dans Visual studio le sln du back
   > Générer le projet (ctrl + shift + B)
1. Ouvrir un git bash dans vs Code et exécuter dans le dossier sources
   ```bash
   bash get-secrets.sh
   ```
1. Ouvrir un git bash dans vs Code et exécuter dans le dossier source
   ```sh
   docker compose up -d
   ```

**Dans Docker Desktop, vous devriez maintenant voir 2 containers dans `kleecontrib-dfta`**

## Debug Front

Pour lancer le script de démarrage qui vérifie les changements en temps réel et recompile les applications (front uniquement), il suffit d'ouvrir un terminal et d'exécuter dans le dossier front :
npm start

## Debug API

Pour l'api (le back c#), il faut recompiler l'api à chaque modification (ctrl + shift + B dans VS)

## Base de donnée

[Modélisation](./model/readme.md)

Pour plus d'information sur les différentes parties, aller voir dans le [README](./README.md).
