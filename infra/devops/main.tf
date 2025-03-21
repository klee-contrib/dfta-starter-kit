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
  personal_access_token = var.devops_pat
}

resource "azuredevops_project" "project" {
  name = var.project_name
}

resource "azuredevops_serviceendpoint_azurerm" "azure" {
  project_id                             = azuredevops_project.project.id
  service_endpoint_name                  = "${var.project_name}-dev"
  service_endpoint_authentication_scheme = "WorkloadIdentityFederation"
  azurerm_spn_tenantid                   = var.tenant_id
  azurerm_subscription_id                = var.subscription_id
  azurerm_subscription_name              = var.subscription_name
}

data "azuredevops_git_repository" "default" {
  name       = var.project_name
  project_id = azuredevops_project.project.id
  depends_on = [azuredevops_project.project]
}

resource "azuredevops_build_definition" "ci" {
  project_id = azuredevops_project.project.id
  name       = "CI"

  ci_trigger {
    use_yaml = true
  }

  variable {
    name           = "version"
    value          = "0.1"
    allow_override = true
  }

  repository {
    repo_type   = "TfsGit"
    repo_id     = data.azuredevops_git_repository.default.id
    branch_name = "refs/heads/main"
    yml_path    = "pipelines/build-ci.yml"
  }

  lifecycle {
    ignore_changes = [variable]
  }
}

resource "azuredevops_build_definition" "pipelines" {
  for_each = {
    "PR"       = "pipelines/build-pr.yml"
    "Reset DB" = "pipelines/reset-db.yml"
    "Recette"  = "pipelines/deploy-recette.yml"
  }

  project_id = azuredevops_project.project.id
  name       = each.key

  ci_trigger {
    use_yaml = true
  }

  repository {
    repo_type   = "TfsGit"
    repo_id     = data.azuredevops_git_repository.default.id
    branch_name = "refs/heads/main"
    yml_path    = each.value
  }
}

resource "azuredevops_pipeline_authorization" "azure" {
  project_id  = azuredevops_project.project.id
  resource_id = azuredevops_serviceendpoint_azurerm.azure.id
  type        = "endpoint"
}
