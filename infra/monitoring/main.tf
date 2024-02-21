resource "azurerm_log_analytics_workspace" "analytics" {
  resource_group_name = var.rg_name
  name                = "${var.app_name}-analytics-${terraform.workspace}"
  location            = var.region

  daily_quota_gb = var.quota
}

resource "azurerm_application_insights" "insights" {
  resource_group_name = var.rg_name
  name                = "${var.app_name}-insights-${terraform.workspace}"
  location            = var.region

  application_type     = "web"
  workspace_id         = azurerm_log_analytics_workspace.analytics.id
  daily_data_cap_in_gb = var.quota
}
