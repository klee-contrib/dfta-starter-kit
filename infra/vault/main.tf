data "azurerm_client_config" "current" {}

resource "azurerm_key_vault" "vault" {
  resource_group_name = var.rg_name
  name                = "${var.app_name}-vault-${terraform.workspace}"
  location            = var.region

  sku_name  = "standard"
  tenant_id = data.azurerm_client_config.current.tenant_id
}

resource "azurerm_key_vault_access_policy" "terraform" {
  key_vault_id = azurerm_key_vault.vault.id
  tenant_id    = azurerm_key_vault.vault.tenant_id
  object_id    = data.azurerm_client_config.current.object_id

  secret_permissions = [
    "List",
    "Get",
    "Set",
    "Delete",
    "Recover",
    "Purge"
  ]
}
