provider "azurerm" {
  subscription_id = var.subscription_id

  resource_provider_registrations = "none"

  features {}
}

provider "azuread" {}


resource "azurerm_resource_group" "rg" {
  name     = terraform.workspace
  location = var.region
}

module "vault" {
  source = "./vault"

  app_name                      = var.app_name
  devops_service_connection_spn = data.terraform_remote_state.devops.outputs.service_connection_spn
  region                        = var.region
  rg_name                       = azurerm_resource_group.rg.name
}

module "vnet" {
  source = "./vnet"

  app_name         = var.app_name
  cidr             = var.vnet_cidr
  devops_vnet_id   = data.terraform_remote_state.devops.outputs.vnet_id
  devops_vnet_name = data.terraform_remote_state.devops.outputs.vnet_name
  devops_rg_name   = data.terraform_remote_state.devops.outputs.rg_name
  region           = var.region
  rg_name          = azurerm_resource_group.rg.name
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

  app_name                      = var.app_name
  devops_service_connection_spn = data.terraform_remote_state.devops.outputs.service_connection_spn
  devops_vnet_id                = data.terraform_remote_state.devops.outputs.vnet_id
  pg_version                    = var.database_pg_version
  region                        = var.region
  rg_name                       = azurerm_resource_group.rg.name
  sku_name                      = var.database_sku_name
  storage_mb                    = var.database_storage_mb
  snet_id                       = module.vnet.snet_db_id
  vault_id                      = module.vault.id
  vnet_id                       = module.vnet.vnet_id
  zone                          = var.database_zone
}

module "storage" {
  source = "./storage"

  app_name = var.app_name
  region   = var.region
  rg_name  = azurerm_resource_group.rg.name
}

module "back" {
  source = "./back"

  aad_client_id        = module.aad.back_client_id
  aad_audience         = module.aad.audience
  ai_connection_string = module.monitoring.connection_string
  app_name             = var.app_name
  database_server_name = module.database.server_name
  region               = var.region
  rg_name              = azurerm_resource_group.rg.name
  sku_name             = var.back_sku_name
  snet_id              = module.vnet.snet_back_id
  storage_account_name = module.storage.account_name
  storage_account_id   = module.storage.account_id
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
