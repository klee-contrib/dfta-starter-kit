# Infra

L'infrastructure est gérée avec [Terraform](https://www.terraform.io/), un outil d'IaC (infrastructure as code) qui permet de décrire un environnement complet et qui est capable de créer et mettre à jour cet environnement dans Azure.

Ce code Terraform va créer les ressources suivantes :

- Un Azure App Service pour le backend
- Une Azure Static App pour le frontend (avec le App Service configuré comme son API)
- Une base de données Azure PostgreSQL avec endpoint privé, connectée au App Service
- Un Azure KeyVault pour stocker les secrets et y accéder.
- Un Azure Application Insights pour les logs applicatifs
- Un projet Azure DevOps avec sa connexion de service pour accéder aux ressources Azure.
- Un VM agent DevOps pour se connecter à la base de données pour les déploiements (et éventuellement servir de rebond pour y accéder).
- Des enregistrements d'applications pour le front et le back dans Azure AD.

Le projet DevOps est créé dans un Terraform séparé, car il n'est pas décliné par environnement.

## Prérequis

Pour construire cette infrastructure, il faut au préalable :

- Un abonnement Azure
- Une organisation Azure DevOps

Un backend Terraform pour stocker son state doit être configuré. Une bonne solution est d'utiliser un compte de stockage (créé manuellement) dans votre abonnement Azure dans un groupe de ressource dédié avec un container pour le state.

Puisqu'il y a deux projets Terraform, il faudra créer un fichier `backend.tf` à la racine et un autre dans le dossier `devops`, qui devraient contenir quelque chose comme :

```tf
terraform {
  backend "azurerm" {
    resource_group_name  = "XXXX"
    storage_account_name = "XXXX"
    container_name       = "tfstate"
    key                  = "terraform(-devops).tfstate"
    subscription_id      = "XXXX"
  }
}
```

Le projet Terraform principal à besoin de référencer le state du Terraform DevOps, afin de pouvoir affecter des droits à DevOps sur les environnements pour le déploiement. Il faudra donc ajouter une data source vers ce state dans le fichier `backend.tf` du projet racine, qui ressemblera à :

```tf
data "terraform_remote_state" "devops" {
  backend = "azurerm"

  config = {
    resource_group_name  = "XXXX"
    storage_account_name = "XXXX"
    container_name       = "tfstate"
    key                  = "terraform-devops.tfstate"
    subscription_id      = "XXXX"
  }
}
```

Pour DevOps, vous aurez aussi besoin de créer un PAT pour que terraform puisse configurer le projet. Il faudra le renseigner dans un fichier `*.auto.tfvars` dans à côté des deux `terraform.tfvars` (le global et celui dans le dossier `devops`). Ces fichiers de variables sont pris en compte automatiquement par Terraform et sont inscrits dans le `.gitignore`.

Il ne faudra pas oublier de renseigner les autres variables demandées dans les deux projets Terraform, qui elles peuvent être ajoutées dans les fichiers `terraform.tfvars` commités car elle ne sont pas secrètes. Pour le projet principal, il s'agit de `subscription_id`, `devops_organisation`, et pour le projet DevOps, `organisation`, `subscription_id` ,`subscription_name` et `tenant_id`.

## Lancement

Enfin, pour lancer le terraform, il faudra au préalable se connecter à Azure via le CLI avec la commande `az login`. Il peut être bon de spécifier le tenant (avec `az login --tenant mytenant.com`) dans le cas où votre compte à accès à plusieurs tenants.

_Remarque : Le tenant choisi doit être celui dans lequel l'abonnement se trouve. Les enregistrements dans Azure AD sont fait dans ce même tenant, mais ce n'est pas obligatoire. Dans ce cas, il faudra spécifier un tenant différent dans la configuration du provider `azuread`._

Le Terraform `devops` doit être lancé en premier avec la commande `terraform apply`. Puisqu'il crée des policies sur la branche principale du repository, il va planter au premier lancement à ce moment-là. Il faudra donc pousser le repository sur le projet nouvellement créé, puis le relancer pour terminer.

Ensuite, vous pouvez lancer `terraform apply` sur le répetoire racine pour créer votre environnement. Il est possible qu'il y ait quelques problèmes à créer l'ensemble de l'environnement du premier coup, donc il ne faut pas hésiter à le relancer...

## Utilisation en local

Certaines ressources crées par Terraform pour un environnement de développement/usine peuvent être également utilisées pour un environnement de développement local, par exemple les enregistrements d'application Azure AD. Le script `get-env.sh` permet de récupérer les infos nécessaires depuis le state terraform et renseigne les fichiers correspondant pour que les applications les utilisent (les user-secrets .NET pour le back, et `sources/front/public/config.json` pour le front).

Le script get-env.sh utilise `jq` qu'il est possible d'installer avec la commande `winget install jqlang.jq`.

## Environnements

Un environnement correspond à un workspace terraform. Ce repo est conçu pour gérer par défaut deux environnements `dev` et `recette` (en particulier côté CI). Utiliser `terraform workspace new dev` et `terraform workspace new recette` pour les créer, puis `select` à la place de `new` pour changer. Pensez à supprimer le workspace `default` par défaut.
