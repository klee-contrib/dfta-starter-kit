output "back_client_id" {
  value       = azuread_application.back.client_id
  description = "Client id back"
}

output "audience" {
  value       = tolist(azuread_application.back.identifier_uris)[0]
  description = "Audience"
}
