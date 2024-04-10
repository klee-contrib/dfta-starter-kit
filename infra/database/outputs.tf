output "server_name" {
  value = azurerm_postgresql_flexible_server.database.fqdn
}
