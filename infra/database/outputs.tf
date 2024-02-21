output "server_name" {
  value = azurerm_postgresql_flexible_server.database.fqdn
}

output "app_secret" {
  value = azurerm_key_vault_secret.database_app_secret.value
}
