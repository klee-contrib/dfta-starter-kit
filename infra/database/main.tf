resource "azurerm_private_dns_zone" "dns_zone" {
  name                = "${terraform.workspace}.${var.app_name}.private.postgres.database.azure.com"
  resource_group_name = var.rg_name
}

resource "azurerm_private_dns_zone_virtual_network_link" "link" {
  name                  = "vnet"
  private_dns_zone_name = azurerm_private_dns_zone.dns_zone.name
  virtual_network_id    = var.vnet_id
  resource_group_name   = var.rg_name
  registration_enabled  = true
}

resource "random_password" "root" {
  length  = 32
  special = false
}

resource "azurerm_postgresql_flexible_server" "database" {
  resource_group_name = var.rg_name
  name                = "${var.app_name}-db-${terraform.workspace}"
  location            = var.region

  administrator_login    = "${var.app_name}_root"
  administrator_password = random_password.root.result
  sku_name               = var.sku_name
  storage_mb             = var.storage_mb
  version                = var.pg_version
  zone                   = var.zone

  delegated_subnet_id = var.snet_id
  private_dns_zone_id = azurerm_private_dns_zone.dns_zone.id
  depends_on          = [azurerm_private_dns_zone_virtual_network_link.link]
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

resource "azurerm_key_vault_secret" "database_admin_secret" {
  key_vault_id = var.vault_id
  name         = "database-admin-secret"
  value        = azurerm_postgresql_flexible_server.database.administrator_password
}
