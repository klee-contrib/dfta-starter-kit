resource "azurerm_storage_account" "storage" {
  resource_group_name = var.rg_name
  name                = "${var.app_name}storage${terraform.workspace}"
  location            = var.region

  account_tier                     = "Standard"
  account_replication_type         = "LRS"
  min_tls_version                  = "TLS1_2"
  allow_nested_items_to_be_public  = false
  cross_tenant_replication_enabled = false
}

resource "azurerm_storage_container" "files" {
  name                  = "files"
  storage_account_id    = azurerm_storage_account.storage.id
  container_access_type = "private"
}
