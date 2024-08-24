## Ces trois variables doivent être renseignées dans un fichier de variables terraform (terraform.tfvars ou *.auto.tfvars).

variable "subscription_id" {
  type        = string
  description = "Id de l'abonnement Azure dans lequel créer les ressources."
  nullable    = false
}

variable "devops_organisation" {
  type        = string
  description = "Nom de l'organisation Azure DevOps."
  nullable    = false
}

## Seule cette variable est un secret qui ne devrait pas être commit. Les fichiers *.auto.tfvars sont dans le .gitignore de ce repository.
variable "devops_pat" {
  type        = string
  description = "Personal Access Token pour se connecter à Azure DevOps."
  nullable    = false
}

#############################

variable "app_name" {
  type        = string
  description = "Nom de l'application, utilisé pour nommé les ressources Azure."
  nullable    = false
}

variable "region" {
  type        = string
  description = "Région Azure dans laquelle déployer les ressources."
  nullable    = false
}

variable "agent_size" {
  type        = string
  description = "Nom du SKU pour la VM de l'agent pour DevOps."
  nullable    = false
}

variable "agent_ip" {
  type        = bool
  description = "Pour déployer une IP publique pour l'agent DevOps afin de s'y connecter en SSH."
  nullable    = false
}

variable "app_insights_quota" {
  type        = number
  description = "Quota de logs (en Go) par jour sur Application Insights, pour maîtriser les coûts."
  nullable    = false
}

variable "back_sku_name" {
  type        = string
  description = "Nom du SKU correspondant à la taille du compute (CPU/RAM) de l'App Service."
  nullable    = false
}

variable "database_sku_name" {
  type        = string
  description = "Nom du SKU correspondant à la taille du compute (CPU/RAM) de la base de données."
  nullable    = false
}

variable "database_storage_mb" {
  type        = number
  description = "Taille du serveur de base de données en Mo."
  nullable    = false
}

variable "database_pg_version" {
  type        = number
  description = "Version de PostgreSQL."
  nullable    = false
}

variable "database_zone" {
  type        = number
  description = "Availability zone de la base de données."
  nullable    = false
}

variable "devops_project_name" {
  type        = string
  description = "Nom du projet Azure DevOps."
  nullable    = false
}

variable "front_region" {
  type        = string
  description = "Région Azure pour l'application web statique (qui n'est disponible que dans un nombre restreint de régions)."
  nullable    = false
}

variable "vnet_cidr" {
  type        = string
  description = "CIDR pour le VNET pour la base de données."
  nullable    = false
}
