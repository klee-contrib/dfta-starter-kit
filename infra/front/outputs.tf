output "url" {
  description = "Url de l'application"
  value       =  azurerm_static_site.front.default_host_name
}
