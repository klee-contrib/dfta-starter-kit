terraform {
  required_providers {
    azuredevops = {
      source  = "microsoft/azuredevops"
      version = ">= 0.1.0"
    }
  }
}

locals {
  admin_user = "admin-${var.app_name}"
}

data "azuredevops_project" "project" {
  name = var.devops_project_name
}

resource "azuredevops_agent_pool" "pool" {
  name = "${var.devops_project_name}-${terraform.workspace}"
}

resource "azuredevops_agent_queue" "queue" {
  project_id    = data.azuredevops_project.project.id
  agent_pool_id = azuredevops_agent_pool.pool.id
}

resource "azuredevops_pipeline_authorization" "auth" {
  project_id  = data.azuredevops_project.project.id
  resource_id = azuredevops_agent_queue.queue.id
  type        = "queue"
}

data "template_cloudinit_config" "agent" {
  gzip          = false
  base64_encode = true

  part {
    filename     = "init.cfg"
    content_type = "text/cloud-config"
    content      = templatefile("${path.module}/files/init.cfg", {})
  }

  part {
    content_type = "text/x-shellscript"
    content = templatefile("${path.module}/files/install.sh", {
      admin_user                    = local.admin_user
      azure_devops_organization_url = "https://dev.azure.com/${var.devops_organisation}"
      agent_name                    = "${terraform.workspace}-001"
      pool_name                     = azuredevops_agent_pool.pool.name
      pat_token                     = var.devops_pat
    })
  }
}

resource "azurerm_public_ip" "ip" {
  count               = var.ip ? 1 : 0
  name                = "${var.app_name}-agent-ip-${terraform.workspace}"
  resource_group_name = var.rg_name
  location            = var.region
  domain_name_label   = "${var.app_name}-agent-${terraform.workspace}"
  allocation_method   = "Static"
  sku                 = "Standard"
}

resource "azurerm_network_security_group" "nsg" {
  name                = "${var.app_name}-agent-nsg-${terraform.workspace}"
  location            = var.region
  resource_group_name = var.rg_name
}

resource "azurerm_network_interface" "nic" {
  name                = "${var.app_name}-agent-nic-${terraform.workspace}"
  location            = var.region
  resource_group_name = var.rg_name

  ip_configuration {
    name                          = "internal"
    subnet_id                     = var.snet_id
    private_ip_address_allocation = "Dynamic"
    public_ip_address_id          = var.ip ? azurerm_public_ip.ip[0].id : null
  }
}

resource "azurerm_network_interface_security_group_association" "agent" {
  network_interface_id      = azurerm_network_interface.nic.id
  network_security_group_id = azurerm_network_security_group.nsg.id
}

resource "tls_private_key" "agent" {
  algorithm = "RSA"
  rsa_bits  = 4096
}

resource "azurerm_key_vault_secret" "agent_private_key" {
  key_vault_id = var.vault_id
  name         = "agent-private-key"
  value        = tls_private_key.agent.private_key_openssh
}

resource "azurerm_linux_virtual_machine" "agent" {
  name                = "${var.app_name}-agent-${terraform.workspace}"
  location            = var.region
  resource_group_name = var.rg_name
  size                = var.size
  admin_username      = local.admin_user

  admin_ssh_key {
    public_key = tls_private_key.agent.public_key_openssh
    username   = local.admin_user
  }

  network_interface_ids = [
    azurerm_network_interface.nic.id
  ]

  user_data = data.template_cloudinit_config.agent.rendered

  os_disk {
    name                 = "${var.app_name}-agent-disk-${terraform.workspace}"
    caching              = "ReadWrite"
    storage_account_type = "Standard_LRS"
  }

  source_image_reference {
    publisher = "Canonical"
    offer     = "ubuntu-24_04-lts"
    sku       = "server"
    version   = "latest"
  }
}
