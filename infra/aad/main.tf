
resource "random_uuid" "aad_scope_id" {}

data "azuread_client_config" "current" {}
data "azuread_application_published_app_ids" "well_known" {}

resource "azuread_application" "back" {
  display_name     = "${var.app_name}-back-${terraform.workspace}"
  sign_in_audience = "AzureADMyOrg"
  owners           = [data.azuread_client_config.current.object_id]

  identifier_uris = ["api://${var.app_name}-back-${terraform.workspace}"]

  api {
    oauth2_permission_scope {
      id                         = random_uuid.aad_scope_id.result
      admin_consent_description  = "Accéder à l'application"
      admin_consent_display_name = "Accéder à l'application"
      type                       = "User"
      user_consent_description   = "Accéder à l'application"
      user_consent_display_name  = "Accéder à l'application"
      value                      = "access"
    }
  }
}

resource "azuread_service_principal" "back" {
  client_id    = azuread_application.back.client_id
  use_existing = true
  owners       = [data.azuread_client_config.current.object_id]
}


resource "azuread_application" "front" {
  display_name     = "${var.app_name}-front-${terraform.workspace}"
  sign_in_audience = "AzureADMyOrg"
  owners           = [data.azuread_client_config.current.object_id]

  api {
    requested_access_token_version = 2
  }

  single_page_application {
    redirect_uris = ["http://localhost/", "https://${var.front_url}/"]
  }

  required_resource_access {
    resource_app_id = data.azuread_application_published_app_ids.well_known.result.MicrosoftGraph

    resource_access {
      id   = "37f7f235-527c-4136-accd-4a02d197296e" // openid
      type = "Scope"
    }

    resource_access {
      id   = "7427e0e9-2fba-42fe-b0c0-848c9e6a8182" // offline_access
      type = "Scope"
    }
  }

  required_resource_access {
    resource_app_id = azuread_application.back.client_id

    resource_access {
      id   = random_uuid.aad_scope_id.result
      type = "Scope"
    }
  }
}

resource "azuread_service_principal" "front" {
  client_id    = azuread_application.front.client_id
  use_existing = true
  owners       = [data.azuread_client_config.current.object_id]
}

resource "azuread_application_pre_authorized" "front" {
  application_id       = azuread_application.back.id
  authorized_client_id = azuread_application.front.client_id
  permission_ids       = [random_uuid.aad_scope_id.result]
}
