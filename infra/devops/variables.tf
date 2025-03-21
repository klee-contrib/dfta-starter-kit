variable "organisation" {
  type     = string
  nullable = false
}

variable "project_name" {
  type     = string
  nullable = false
}

## Ces quatre variables doivent être renseignées dans un fichier de variables terraform (terraform.tfvars ou *.auto.tfvars).

variable "tenant_id" {
  type        = string
  description = "Id du tenant Azure, dans lequel se trouve l'abonnement."
  nullable    = false
}

variable "subscription_id" {
  type        = string
  description = "Id de l'abonnement Azure dans lequel créer les ressources."
  nullable    = false
}

variable "subscription_name" {
  type        = string
  description = "Nom de l'abonnement Azure dans lequel créer les ressources."
  nullable    = false
}

## Seule cette variable est un secret qui ne devrait pas être commit. Les fichiers *.auto.tfvars sont dans le .gitignore de ce repository.
variable "devops_pat" {
  type        = string
  description = "Personal Access Token pour se connecter à Azure DevOps."
  nullable    = false
}
