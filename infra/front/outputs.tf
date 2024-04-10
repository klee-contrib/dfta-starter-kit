output "url" {
  description = "Url de l'application"
  value       =  azurerm_static_web_app.front.default_host_name
}
