resource "azurerm_resource_group" "rg" {
  name     = "devops"
  location = var.region
}

resource "azurerm_dev_center" "center" {
  name                = "${var.organisation}-dev-center"
  resource_group_name = azurerm_resource_group.rg.name
  location            = var.region
}

resource "azurerm_dev_center_project" "project" {
  name                = "${var.organisation}-${lower(var.project_name)}-project"
  resource_group_name = azurerm_resource_group.rg.name
  location            = var.region
  dev_center_id       = azurerm_dev_center.center.id
}

resource "azurerm_virtual_network" "vnet" {
  name                = "${var.organisation}-${lower(var.project_name)}-vnet"
  location            = var.region
  resource_group_name = azurerm_resource_group.rg.name
  address_space       = [var.vnet_cidr]

  lifecycle {
    ignore_changes = [subnet]
  }
}

resource "azurerm_subnet" "pool" {
  name                 = "${var.organisation}-${lower(var.project_name)}-pool-snet"
  resource_group_name  = azurerm_resource_group.rg.name
  virtual_network_name = azurerm_virtual_network.vnet.name
  address_prefixes     = [cidrsubnet(var.vnet_cidr, 4, 1)]

  delegation {
    name = "pool"

    service_delegation {
      actions = [
        "Microsoft.Network/virtualNetworks/subnets/join/action",
      ]
      name = "Microsoft.DevOpsInfrastructure/pools"
    }
  }
}

data "azuread_service_principal" "devops_infra" {
  display_name = "DevOpsInfrastructure"
}

resource "azurerm_role_assignment" "devops_reader" {
  scope                = azurerm_virtual_network.vnet.id
  role_definition_name = "Reader"
  principal_id         = data.azuread_service_principal.devops_infra.object_id
}

resource "azurerm_role_assignment" "devops_network_contributor" {
  scope                = azurerm_virtual_network.vnet.id
  role_definition_name = "Network Contributor"
  principal_id         = data.azuread_service_principal.devops_infra.object_id
}

resource "azurerm_managed_devops_pool" "pool" {
  depends_on = [azurerm_role_assignment.devops_network_contributor, azurerm_role_assignment.devops_reader]

  name                  = "${var.organisation}-${lower(var.project_name)}-pool"
  resource_group_name   = azurerm_resource_group.rg.name
  location              = var.region
  dev_center_project_id = azurerm_dev_center_project.project.id
  maximum_concurrency   = 1

  azure_devops_organization {
    organization {
      parallelism = 1
      url         = "https://dev.azure.com/${var.organisation}"
    }
  }

  stateless_agent {}

  virtual_machine_scale_set_fabric {
    sku_name                     = var.pool_sku_name
    subnet_id                    = azurerm_subnet.pool.id
    os_disk_storage_account_type = "StandardSSD"

    image {
      well_known_image_name = "ubuntu-24.04-g2/latest"
    }

    storage {
      disk_size_in_gb = 5
    }
  }

  work_folder = "/mnt/storage/sdc/_work"
}

data "azuredevops_agent_queue" "queue" {
  name       = azurerm_managed_devops_pool.pool.name
  project_id = azuredevops_project.project.id
}

resource "azuredevops_pipeline_authorization" "auth" {
  project_id  = azuredevops_project.project.id
  resource_id = data.azuredevops_agent_queue.queue.id
  type        = "queue"
}
