terraform {
  required_providers {
    azuredevops = {
      source  = "microsoft/azuredevops"
      version = ">= 0.1.0"
    }
  }
}

provider "azuredevops" {
  org_service_url       = "https://dev.azure.com/${var.organisation}"
  personal_access_token = var.pat
}

resource "azuredevops_project" "project" {
  name = var.project_name
}

data "azurerm_client_config" "current" {}
data "azurerm_subscription" "current" {}

resource "azuredevops_serviceendpoint_azurerm" "azure" {
  project_id                             = azuredevops_project.project.id
  service_endpoint_name                  = "${var.project_name}-${data.azurerm_client_config.current.subscription_id}"
  service_endpoint_authentication_scheme = "WorkloadIdentityFederation"
  azurerm_spn_tenantid                   = data.azurerm_client_config.current.tenant_id
  azurerm_subscription_id                = data.azurerm_client_config.current.subscription_id
  azurerm_subscription_name              = data.azurerm_subscription.current.display_name
}

data "azuread_service_principal" "devops" {
  display_name = "${var.organisation}-${var.project_name}-${data.azurerm_client_config.current.subscription_id}"
  depends_on   = [azuredevops_serviceendpoint_azurerm.azure]
}

resource "azurerm_key_vault_access_policy" "devops" {
  key_vault_id = var.vault_id
  tenant_id    = data.azurerm_client_config.current.tenant_id
  object_id    = data.azuread_service_principal.devops.object_id

  secret_permissions = [
    "List",
    "Get"
  ]
}
