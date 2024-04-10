terraform {
  required_providers {
    azuredevops = {
      source  = "microsoft/azuredevops"
      version = ">= 0.1.0"
    }
  }
}

provider "azurerm" {
  subscription_id = var.subscription_id
  features {}
}

provider "azuread" {}

provider "azuredevops" {
  org_service_url       = "https://dev.azure.com/${var.devops_organisation}"
  personal_access_token = var.devops_pat
}

module "devops" {
  count  = terraform.workspace == "dev" ? 1 : 0
  source = "./devops"

  organisation = var.devops_organisation
  project_name = var.devops_project_name
}

resource "azurerm_resource_group" "rg" {
  name     = terraform.workspace
  location = var.region
}

module "vault" {
  source = "./vault"

  app_name            = var.app_name
  devops_organisation = var.devops_organisation
  devops_project_name = var.devops_project_name
  region              = var.region
  rg_name             = azurerm_resource_group.rg.name
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

  app_name            = var.app_name
  devops_organisation = var.devops_organisation
  devops_project_name = var.devops_project_name
  pg_version          = var.database_pg_version
  region              = var.region
  rg_name             = azurerm_resource_group.rg.name
  sku_name            = var.database_sku_name
  storage_mb          = var.database_storage_mb
  snet_id             = module.vnet.snet_db_id
  vault_id            = module.vault.id
  vnet_id             = module.vnet.vnet_id
  zone                = var.database_zone
}

module "back" {
  source = "./back"

  aad_client_id        = module.aad.back_client_id
  aad_audience         = module.aad.audience
  ai_key               = module.monitoring.instrumentation_key
  app_name             = var.app_name
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
  vault_id  = module.vault.id
}

module "agent" {
  source = "./agent"

  app_name            = var.app_name
  devops_organisation = var.devops_organisation
  devops_pat          = var.devops_pat
  devops_project_name = var.devops_project_name
  region              = var.region
  rg_name             = azurerm_resource_group.rg.name
  snet_id             = module.vnet.snet_agent_id
  size                = var.agent_size
  vault_id            = module.vault.id
}
