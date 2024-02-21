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

## Prérequis

Pour construire cette infrastructure, il faut au préalable :

- Un abonnement Azure
- Une organisation Azure DevOps

Un backend Terraform pour stocker son state doit être configuré. Une bonne solution est d'utiliser un compte de stockage (créé manuellement) dans votre abonnement Azure dans un groupe de ressource dédié avec un container pour le state.

Vous pouvez l'ajouter dans un fichier `backend.tf`, qui devrait contenir quelque chose comme

```tf
terraform {
  backend "azurerm" {
    resource_group_name  = "XXXX"
    storage_account_name = "XXXX"
    container_name       = "tfstate"
    key                  = "terraform.tfstate"
    subscription_id      = "XXXX"
  }
}
```

Pour DevOps, vous aurez aussi besoin de créer un PAT pour que terraform puisse configurer le projet.

En plus des variables pré-définies dans le fichier `terraform.tfvars` (que vous pouvez modifier), vous devez ajouter un autre fichier de variables (par exemple `config.auto.tfvars`, pour qu'il soit pris automatiquement lors d'un `terraform apply`), dans lequel il faudra renseigner les variables `subscription_id`, `devops_organisation` et `devops_pat`. Attention à ne pas commit la dernière variable 😉

## Lancement

Enfin, pour lancer le terraform, il faudra au préalable se connecter à Azure via le CLI avec la commande `az login`. Il peut être bon de spécifier le tenant (avec `az login --tenant mytenant.com`) dans le cas où votre compte à accès à plusieurs tenants.

_Remarque : Le tenant choisi doit être celui dans lequel l'abonnement se trouve. Les enregistrements dans Azure AD sont fait dans ce même tenant, mais ce n'est pas obligatoire. Dans ce cas, il faudra spécifier un tenant différent dans la configuration du provider `azuread`._

## Utilisation en local

Certaines ressources crées par Terraform pour un environnement de développement/usine peuvent être également utilisées pour un environnement de développement local, par exemple les enregistrements d'application Azure AD. Le script `get-env.sh` permet de récupérer les infos nécessaires depuis le state terraform et renseigne les fichiers correspondant pour que les applications les utilisent (le fichier `sources/.env` pour le back, et `sources/front/public/config.json` pour le front).
