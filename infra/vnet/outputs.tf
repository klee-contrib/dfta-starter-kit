output "vnet_id" {
  value       = azurerm_virtual_network.vnet.id
  description = "Vnet ID"
}

output "snet_back_id" {
  value       = azurerm_subnet.back.id
  description = "Back Subnet ID"
}

output "snet_db_id" {
  value       = azurerm_subnet.db.id
  description = "Database Subnet ID"
}
