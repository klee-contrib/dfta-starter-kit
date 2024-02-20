# Applicatifs

L'application est divisée en deux parties :

- [Un applicatif ASP.NET Core (et une BDD PostgreSQL) pour servir les APIs pour le back](./back/readme.md)
- [Une application Focus pour le front](./front/readme.md)

## Mise en route

En dev, le back tourne intégralement dans Docker via Docker Compose. 2 services sont configurés :

- L'API, sur le port 2468
- La base de données, sur le port 5432

Le front se lance directement via un `npm start` dans son répertoire, et sera accessible sur le port 1357. Un proxy est configuré sur le serveur front pour que les appels d'APIs soient redirigés vers le back.

## Configuration

La configuration du back se fait via des variables d'environnement configurées dans le docker compose. La plupart de la configuration n'est également pas sensible (la base de données étant en local, on se fiche pas mal du mot de passe, et ce n'est évidemment pas géré de la même manière en vrai), à l'exception du secret qui permet de se connecter aux enregistrements d'applications dans Azure AD.

Pour récupérer ces secrets, il faut lancer le script `get-secrets.sh` qui se connecte à Azure pour aller chercher les secrets dans le Key Vault de dev. Il faut se connecter avec son propre compte, ce qui nécessite d'avoir été affecté avec les bons droits au préalable.
