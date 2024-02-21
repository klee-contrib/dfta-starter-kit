resource "random_password" "app" {
  length  = 32
  special = false
}

resource "azurerm_key_vault_secret" "database_app_secret" {
  key_vault_id = var.vault_id
  name         = "database-app-secret"
  value        = random_password.app.result
}

resource "random_password" "read" {
  length  = 32
  special = false
}

resource "azurerm_key_vault_secret" "database_read_secret" {
  key_vault_id = var.vault_id
  name         = "database-read-secret"
  value        = random_password.read.result
}
