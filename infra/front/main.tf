data "azurerm_client_config" "current" {}

resource "azurerm_static_web_app" "front" {
  name                = "${var.app_name}-front-${terraform.workspace}"
  resource_group_name = var.rg_name
  location            = var.front_region
  sku_size            = "Standard"
  sku_tier            = "Standard"

  lifecycle {
    ignore_changes = [repository_branch, repository_url]
  }
}

resource "azurerm_key_vault_secret" "front" {
  key_vault_id = var.vault_id
  name         = "front-api-key"
  value        = azurerm_static_web_app.front.api_key
}

resource "null_resource" "front_back_link" {
  provisioner "local-exec" {
    command = "az account set --subscription ${data.azurerm_client_config.current.subscription_id}"
  }
  provisioner "local-exec" {
    command = "az staticwebapp backends link --backend-resource-id ${var.back_id} --name ${azurerm_static_web_app.front.name} --resource-group ${var.rg_name} --backend-region ${var.back_region}"
  }
}
