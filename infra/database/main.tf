data "azurerm_client_config" "current" {
}

data "azuread_user" "current" {
  object_id = data.azurerm_client_config.current.object_id
}

resource "azurerm_private_dns_zone" "dns_zone" {
  name                = "privatelink.postgres.database.azure.com"
  resource_group_name = var.rg_name
}

resource "azurerm_private_dns_zone_virtual_network_link" "link" {
  name                  = "vnet"
  private_dns_zone_name = azurerm_private_dns_zone.dns_zone.name
  virtual_network_id    = var.vnet_id
  resource_group_name   = var.rg_name
  registration_enabled  = true
}

resource "azurerm_postgresql_flexible_server" "database" {
  resource_group_name = var.rg_name
  name                = "${var.app_name}-db-${terraform.workspace}"
  location            = var.region

  public_network_access_enabled = false

  sku_name   = var.sku_name
  storage_mb = var.storage_mb
  version    = var.pg_version
  zone       = var.zone


  authentication {
    active_directory_auth_enabled = true
    password_auth_enabled         = false
    tenant_id                     = data.azurerm_client_config.current.tenant_id
  }
}

resource "azurerm_private_endpoint" "database" {
  name                = "${var.app_name}-db-${terraform.workspace}-pe"
  location            = var.region
  resource_group_name = var.rg_name
  subnet_id           = var.snet_id

  private_service_connection {
    name                           = "database"
    private_connection_resource_id = azurerm_postgresql_flexible_server.database.id
    subresource_names              = ["postgresqlServer"]
    is_manual_connection           = false
  }

  private_dns_zone_group {
    name                 = "database"
    private_dns_zone_ids = [azurerm_private_dns_zone.dns_zone.id]
  }
}

resource "azurerm_postgresql_flexible_server_active_directory_administrator" "terraform" {
  server_name         = azurerm_postgresql_flexible_server.database.name
  resource_group_name = var.rg_name
  tenant_id           = data.azurerm_client_config.current.tenant_id
  object_id           = data.azurerm_client_config.current.object_id
  principal_name      = data.azuread_user.current.mail
  principal_type      = "User"
}

data "azuread_service_principal" "devops" {
  # Il semblerait que la dernière partie soit désormais l'id de la service connexion et plus celui de l'abonnement...
  display_name = "${var.devops_organisation}-${var.devops_project_name}-${data.azurerm_client_config.current.subscription_id}"
}

resource "azurerm_postgresql_flexible_server_active_directory_administrator" "devops" {
  server_name         = azurerm_postgresql_flexible_server.database.name
  resource_group_name = var.rg_name
  tenant_id           = data.azurerm_client_config.current.tenant_id
  object_id           = data.azuread_service_principal.devops.object_id
  principal_name      = data.azuread_service_principal.devops.display_name
  principal_type      = "ServicePrincipal"
}

resource "azurerm_postgresql_flexible_server_configuration" "lc_monetary" {
  server_id = azurerm_postgresql_flexible_server.database.id
  name      = "lc_monetary"
  value     = "fr_FR.utf8"
}

resource "azurerm_postgresql_flexible_server_configuration" "lc_numeric" {
  server_id = azurerm_postgresql_flexible_server.database.id
  name      = "lc_numeric"
  value     = "fr_FR.utf8"
}

resource "azurerm_postgresql_flexible_server_configuration" "DateStyle" {
  server_id = azurerm_postgresql_flexible_server.database.id
  name      = "DateStyle"
  value     = "ISO, DMY"
}

resource "azurerm_postgresql_flexible_server_configuration" "extensions" {
  server_id = azurerm_postgresql_flexible_server.database.id
  name      = "azure.extensions"
  value     = "unaccent,uuid-ossp"
}
