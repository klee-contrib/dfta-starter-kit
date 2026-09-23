# Infra

L'infrastructure est gérée avec [Terraform](https://www.terraform.io/), un outil d'IaC (infrastructure as code) qui permet de décrire un environnement complet et qui est capable de créer et mettre à jour cet environnement dans Azure.

Ce code Terraform crée les ressources suivantes :

- Un Azure App Service pour le back
- Une Azure Static Web App pour le front (avec l'App Service configuré comme API)
- Une base de données Azure Database for PostgreSQL avec accès privé depuis l'App Service
- Un Azure Key Vault pour les secrets
- Un Azure Application Insights pour les logs applicatifs
- Un projet Azure DevOps et une connexion de service pour accéder aux ressources Azure
- Un pool d'agents Azure DevOps managé, relié au réseau virtuel pour exécuter les migrations de base de données pendant les déploiements
- Des enregistrements d'applications pour le front et le back dans Microsoft Entra ID

Le projet DevOps est créé dans un Terraform séparé, car il n'est pas décliné par environnement.

## Prérequis

Pour construire cette infrastructure, prévoyez :

- Un abonnement Azure
- Une organisation Azure DevOps

Un backend Terraform doit être configuré pour stocker le state. Par exemple, utilisez un compte de stockage créé dans un groupe de ressources dédié de l'abonnement Azure, avec un conteneur pour le state.

Créez un fichier `backend.tf` à la racine de `infra` et un autre dans `infra/devops`, puis configurez-les avec les paramètres de votre abonnement avant `terraform init`. Utilisez `terraform.tfstate` pour la clé du backend principal et `terraform-devops.tfstate` pour celui de DevOps. Exemple pour le projet principal :

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

_Remarque : les fichiers `backend.tf` sont dans le `.gitignore` du starter kit pour ne pas les commiter ici. Pour les versionner dans votre projet, retirez `backend.tf` du `.gitignore`._

Le projet Terraform principal a besoin de référencer le state du Terraform DevOps afin de pouvoir affecter des droits à DevOps sur les environnements pour le déploiement. Ajoutez également la data source `terraform_remote_state.devops` dans `infra/backend.tf`, en l'adaptant au backend DevOps choisi :

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

Pour DevOps, créez un PAT pour que Terraform puisse configurer le projet. Renseignez `devops_pat` dans `infra/devops/*.auto.tfvars` uniquement ; le projet Terraform principal n'utilise pas ce PAT. Les fichiers `*.auto.tfvars` sont pris en compte automatiquement par Terraform et ignorés par Git. Ne publiez pas le PAT, y compris dans les logs ou les exemples de configuration.

Renseignez aussi les variables d'abonnement dans les fichiers locaux `terraform.tfvars` (si ce n'est pas un secret) et `*.auto.tfvars` (si c'est un secret) : `subscription_id` dans `infra`, et `organisation`, `subscription_id`, `subscription_name`, `tenant_id` dans `infra/devops`. La liste complète figure dans le fichier `variables.tf` de chaque projet.

## Lancement

Avant de lancer Terraform, connectez-vous à Azure avec `az login`. Si votre compte a accès à plusieurs tenants, précisez celui de l'abonnement avec `az login --tenant mytenant.com`.

_Remarque : par défaut, les enregistrements d'applications Microsoft Entra ID sont créés dans le tenant de l'abonnement. Pour utiliser un autre tenant, adaptez la configuration du provider `azuread`._

Initialisez puis appliquez d'abord le Terraform `devops` avec `terraform init` et `terraform apply` depuis `infra/devops`. Puisqu'il crée des policies sur la branche principale du repository, l'apply peut échouer au premier lancement à ce moment-là. Poussez alors le repository sur le projet nouvellement créé et relancez `terraform apply`.

Ensuite, lancez `terraform init`, sélectionnez le workspace voulu et exécutez `terraform apply` depuis `infra` pour créer l'environnement. Le state Terraform DevOps doit être disponible avant cette étape.

## Utilisation en local

Certaines ressources créées par Terraform pour l'environnement `dev` sont également utilisées en local, notamment les enregistrements d'application Entra ID et le compte de stockage. Après `terraform init` et `az login`, exécutez `./get-env.sh` depuis `infra` : il sélectionne le workspace `dev`, lit le state et renseigne les user-secrets .NET du back et `sources/front/public/config.json` pour le front. L'accès au state et la commande `jq` sont nécessaires.

## Environnements

Un environnement correspond à un workspace Terraform. Ce dépôt prévoit deux environnements, `dev` et `recette`, utilisés notamment par la CI/CD. Créez-les avec `terraform workspace new dev` et `terraform workspace new recette`, puis sélectionnez-les avec `terraform workspace select dev` ou `terraform workspace select recette`. Le workspace `default` n'est pas utilisé.
