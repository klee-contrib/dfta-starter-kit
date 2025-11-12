# Starter Kit .NET - Focus - Topmodel - Azure

Ce starter kit permet d'initialiser une application fullstack avec gestion des sources, CI/CD et des environnements complets sur Azure.

Il utilise :

- Une API .NET 9, qui utilise quelques modules de [kinetix](https://github.com/klee-contrib/kinetix), avec une base de données PostgreSQL
- Une application front [focus4](https://github.com/klee-contrib/focus4)
- Un modèle géré avec [topmodel](https://github.com/klee-contrib/topmodel)
- Une infrastructure Azure gérée avec Terraform
- Un projet Azure DevOps configuré avec la CI/CD pour déployer dans l'infrastructure précédente

L'authentification est configurée pour utiliser l'Azure AD lié au tenant de l'abonnement Azure utilisé.

Pour démarrer un projet avec ce starter kit, vous avez besoin :

- D'un abonnement Azure. Vous pouvez utiliser des crédits gratuits ou bien les crédits lié à votre abonnement MSDN, si vous en avez un, pour commencer.
- D'une organisation Azure DevOps. Vous pouvez la créer gratuitement [par ici](https://azure.microsoft.com/fr-fr/products/devops/). Le niveau gratuit inclus 5 utilisateurs (sans compter ceux qui ont un abonnement MSDN qui ont une licence incluse) et 30h de build par mois.

Vous pouvez ensuite lancer le terraform qui va créer le projet Azure DevOps et un environnement de dev, puis push le code sur votre nouveau repository sur DevOps.

## Initialisation des sources

Renommer le nom du client et du projet dans les sources avec cette commande à la racine du repo : 

```sh
dotnet run rename-project.cs MonClient MonProjet MonAppli
```

## Rubriques

- [Installation](./InstallAndDebug.md)
- [Modélisation](./model/readme.md)
- [Applicatifs](./sources/readme.md)
- [Migrations de base de données](./migrations/readme.md)
- [Infrastructure](./infra/readme.md)
