resource "azurerm_virtual_network" "vnet" {
  name                = "${var.app_name}-vnet-${terraform.workspace}"
  location            = var.region
  resource_group_name = var.rg_name
  address_space       = [var.cidr]

  lifecycle {
    ignore_changes = [subnet]
  }
}

resource "azurerm_subnet" "back" {
  name                              = "${var.app_name}-snet-back-${terraform.workspace}"
  resource_group_name               = var.rg_name
  virtual_network_name              = azurerm_virtual_network.vnet.name
  address_prefixes                  = [cidrsubnet(var.cidr, 8, 1)]
  private_endpoint_network_policies = "Enabled"

  delegation {
    name = "app-service"

    service_delegation {
      actions = [
        "Microsoft.Network/virtualNetworks/subnets/action",
      ]
      name = "Microsoft.Web/serverFarms"
    }
  }
}

resource "azurerm_subnet" "db" {
  name                              = "${var.app_name}-snet-db-${terraform.workspace}"
  resource_group_name               = var.rg_name
  virtual_network_name              = azurerm_virtual_network.vnet.name
  address_prefixes                  = [cidrsubnet(var.cidr, 8, 2)]
  private_endpoint_network_policies = "Enabled"
}

resource "azurerm_subnet" "agent" {
  name                              = "${var.app_name}-snet-agent-${terraform.workspace}"
  resource_group_name               = var.rg_name
  virtual_network_name              = azurerm_virtual_network.vnet.name
  address_prefixes                  = [cidrsubnet(var.cidr, 8, 3)]
  private_endpoint_network_policies = "Enabled"
}
