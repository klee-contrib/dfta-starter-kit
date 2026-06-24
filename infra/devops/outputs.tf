output "service_connection_spn" {
  value       = "${var.organisation}-${var.project_name}-${azuredevops_serviceendpoint_azurerm.azure.id}"
  description = "Id du projet DevOps"
}

output "vnet_id" {
  value       = azurerm_virtual_network.vnet.id
  description = "ID du VNET du pool"
}

output "vnet_name" {
  value       = azurerm_virtual_network.vnet.name
  description = "Nom du VNET du pool"
}

output "rg_name" {
  value       = azurerm_resource_group.rg.name
  description = "Nom du RG du pool"
}

