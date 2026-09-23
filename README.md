# Starter Kit .NET - Focus4 - TopModel - Azure

Ce starter kit permet d'initialiser une application full-stack avec un projet Azure DevOps, une CI/CD et des environnements sur Azure.

Il utilise :

- Une API .NET 10 utilisant des modules de [Kinetix](https://github.com/klee-contrib/kinetix) et une base de données PostgreSQL
- Une application front [Focus4](https://github.com/klee-contrib/focus4)
- Un modèle géré avec [TopModel](https://github.com/klee-contrib/topmodel)
- Une infrastructure Azure gérée avec Terraform
- Un projet Azure DevOps configuré avec la CI/CD pour déployer dans l'infrastructure précédente

L'authentification utilise Microsoft Entra ID dans le tenant de l'abonnement Azure.

Pour démarrer un projet avec ce starter kit, prévoyez :

- Un abonnement Azure. Utilisez des crédits gratuits ou ceux de votre abonnement MSDN, si vous en avez un, pour commencer.
- D'une organisation Azure DevOps, à créer [ici](https://azure.microsoft.com/fr-fr/products/devops/) si nécessaire.

Le Terraform du dossier `infra/devops` crée le projet Azure DevOps. Une fois le code poussé dans le nouveau dépôt, le Terraform du dossier `infra` permet de créer les environnements. Les étapes sont détaillées dans [Infrastructure](./infra/readme.md).

## Initialisation des sources

Pour renommer le client, le projet et l'application dans le dépôt, exécutez cette commande à sa racine :

```sh
dotnet run rename-project.cs MonClient MonProjet MonAppli
```

## Rubriques

- [Installation](./Installation.md)
- [Modélisation](./model/readme.md)
- [Applicatifs](./sources/readme.md)
- [Migrations de base de données](./migrations/readme.md)
- [Infrastructure](./infra/readme.md)
