data "azuread_client_config" "current" {}
data "azurerm_client_config" "current" {}

resource "azurerm_service_plan" "back" {
  name                = "${var.app_name}-api-${terraform.workspace}"
  resource_group_name = var.rg_name
  location            = var.region
  os_type             = "Linux"
  sku_name            = var.sku_name
  worker_count        = 1
}

resource "azurerm_linux_web_app" "back" {
  name                      = "${var.app_name}-api-${terraform.workspace}"
  resource_group_name       = var.rg_name
  location                  = var.region
  service_plan_id           = azurerm_service_plan.back.id
  https_only                = true
  virtual_network_subnet_id = var.snet_id

  lifecycle {
    ignore_changes = [auth_settings_v2]
  }

  site_config {
  }

  identity {
    type = "SystemAssigned"
  }

  app_settings = {
    ASPNETCORE_ENVIRONMENT = title(terraform.workspace)

    ApplicationInsights__InstrumentationKey = var.ai_key

    AzureAd__Audience = var.aad_audience
    AzureAd__ClientId = var.aad_client_id
    AzureAd__TenantId = data.azuread_client_config.current.tenant_id

    Database__Server  = var.database_server_name
    Database__User_Id = "${var.app_name}-api-${terraform.workspace}"

    Storage__AccountName = var.storage_account_name
  }
}

resource "azurerm_role_assignment" "back_storage" {
  principal_id       = azurerm_linux_web_app.back.identity[0].principal_id
  role_definition_id = "/subscriptions/${data.azurerm_client_config.current.subscription_id}/providers/Microsoft.Authorization/roleDefinitions/ba92f5b4-2d11-453d-a403-e96b0029c9fe" # Contributeur aux données Blob du stockage
  scope              = var.storage_account_id
}
