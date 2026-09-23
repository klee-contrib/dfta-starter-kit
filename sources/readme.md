# Applicatifs

L'application est divisée en deux parties :

- [Une API ASP.NET Core utilisant PostgreSQL pour le back](./back/readme.md)
- [Une application Focus4 pour le front](./front/readme.md)

## Mise en route

En local, récupérez d'abord la configuration de l'environnement `dev` créé par [Terraform](../infra/readme.md). Après `terraform init` et `az login`, lancez `./get-env.sh` depuis le dossier `infra`. Le script sélectionne le workspace `dev` et renseigne les paramètres Microsoft Entra ID et du compte de stockage dans les user-secrets .NET du back, ainsi que la configuration du front dans `sources/front/public/config.json`.

Pour le reste :

- Lancez la base de données dans Docker avec `docker compose up -d` depuis le dossier `sources` (sur le port 5432). Une fois la base démarrée, lancez `dotnet run` dans le dossier [`migrations`](../migrations/readme.md) pour appliquer les migrations. Le profil local utilise `--reset if-impossible` : il peut recréer la base si ses migrations ne sont plus disponibles.
- Lancez l'API depuis Visual Studio avec le projet `sources/back/KleeContrib.Dfta.Api`, ou avec `dotnet run` depuis ce dossier. Elle est accessible sur le port 2468.
- Lancez le front avec `npm ci`, puis `npm start` depuis le dossier `sources/front`. Il est accessible sur le port 1357 ; un proxy redirige les appels `/api` vers l'API.
