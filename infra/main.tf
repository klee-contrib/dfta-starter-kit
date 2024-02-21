variable "subscription_id" {
  type     = string
  nullable = false
}

variable "app_name" {
  type     = string
  nullable = false
}

variable "region" {
  type     = string
  nullable = false
}

variable "front_region" {
  type     = string
  nullable = false
}

variable "app_insights_quota" {
  type     = number
  nullable = false
}

variable "vnet_cidr" {
  type     = string
  nullable = false
}

variable "database_storage_mb" {
  type     = number
  nullable = false
}

variable "database_sku_name" {
  type     = string
  nullable = false
}

variable "database_pg_version" {
  type     = number
  nullable = false
}

variable "database_zone" {
  type     = number
  nullable = false
}

variable "back_sku_name" {
  type     = string
  nullable = false
}

# variable "devops_pat" {
#   type     = string
#   nullable = false
# }

provider "azurerm" {
  subscription_id = var.subscription_id
  features {}
}

provider "azuread" {}

resource "azurerm_resource_group" "rg" {
  name     = terraform.workspace
  location = var.region
}

module "vault" {
  source = "./vault"

  app_name = var.app_name
  region   = var.region
  rg_name  = azurerm_resource_group.rg.name
}

module "vnet" {
  source = "./vnet"

  app_name = var.app_name
  cidr     = var.vnet_cidr
  region   = var.region
  rg_name  = azurerm_resource_group.rg.name
}

module "monitoring" {
  source = "./monitoring"

  app_name = var.app_name
  quota    = var.app_insights_quota
  region   = var.region
  rg_name  = azurerm_resource_group.rg.name
}

module "database" {
  source = "./database"

  app_name   = var.app_name
  pg_version = var.database_pg_version
  region     = var.region
  rg_name    = azurerm_resource_group.rg.name
  sku_name   = var.database_sku_name
  storage_mb = var.database_storage_mb
  snet_id    = module.vnet.snet_db_id
  vault_id   = module.vault.id
  vnet_id    = module.vnet.vnet_id
  zone       = var.database_zone
}

module "back" {
  source = "./back"

  aad_client_id        = module.aad.back_client_id
  aad_audience         = module.aad.audience
  ai_key               = module.monitoring.instrumentation_key
  app_name             = var.app_name
  database_app_secret  = module.database.app_secret
  database_server_name = module.database.server_name
  region               = var.region
  rg_name              = azurerm_resource_group.rg.name
  sku_name             = var.back_sku_name
  snet_id              = module.vnet.snet_back_id
}

module "front" {
  source = "./front"

  app_name     = var.app_name
  back_region  = var.region
  back_id      = module.back.back_id
  front_region = var.front_region
  rg_name      = azurerm_resource_group.rg.name
  vault_id     = module.vault.id
}

module "aad" {
  source = "./aad"

  app_name  = var.app_name
  front_url = module.front.url
}

# module "devops" {
#   source   = "./devops"
#   pat      = var.devops_pat
#   vault_id = module.vault.id
# }

# module "agent" {
#   source   = "./agent"
#   snet_id  = module.vnet.snet_agent_id
#   vault_id = module.vault.id
#   rg_name  = azurerm_resource_group.rg.name
#   pat      = var.devops_pat
# }
