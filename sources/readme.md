# Applicatifs

L'application est divisée en deux parties :

- [Un applicatif ASP.NET Core (et une BDD PostgreSQL) pour servir les APIs pour le back](./back/readme.md)
- [Une application Focus pour le front](./front/readme.md)

## Mise en route

En local, il est en premier lieu nécessaire de récupérer la configuration de l'environnement de dev créé par le Terraform du dossier [`infra`](../infra/readme.md), en lançant le script `get-env.sh`. Elle sera utilisée pour récupérer les infos de connexion à Entra ID et au compte de stockage, qui ne seront pas reproductible sur un poste de développeur en local comme le reste des composants.

Pour le reste :

- La base de données tourne dans Docker et se lance via Docker Compose (sur le port 5432). Une fois démarée, lancer le script `dev-recreate-db.sh` dans le dossier [`migrations`](../migrations/readme.md) pour l'initialiser.
- L'API se lance via le bouton "Play" dans Visual Studio sur le projet `sources/back/KleeContrib.Dfta.Api`, ou bien en lançant la commande `dotnet run` dans le dossier de ce projet. Elle est lancée sur le port 2468.
- Le front se lance directement via un `npm start` dans son répertoire, et sera accessible sur le port 1357. Un proxy est configuré sur le serveur front pour que les appels d'APIs soient redirigés vers le back.
