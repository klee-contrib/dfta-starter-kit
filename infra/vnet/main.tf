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

resource "azurerm_virtual_network_peering" "from_devops" {
  name                      = "devops-to-${terraform.workspace}"
  resource_group_name       = var.devops_rg_name
  virtual_network_name      = var.devops_vnet_name
  remote_virtual_network_id = azurerm_virtual_network.vnet.id
}

resource "azurerm_virtual_network_peering" "to_devops" {
  name                         = "${terraform.workspace}-to-devops"
  resource_group_name          = var.rg_name
  virtual_network_name         = azurerm_virtual_network.vnet.name
  remote_virtual_network_id    = var.devops_vnet_id
  allow_virtual_network_access = false
}
