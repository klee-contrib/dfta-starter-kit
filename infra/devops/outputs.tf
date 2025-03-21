output "service_connection_spn" {
  value       = "${var.organisation}-${var.project_name}-${azuredevops_serviceendpoint_azurerm.azure.id}"
  description = "Id du projet DevOps"
}
