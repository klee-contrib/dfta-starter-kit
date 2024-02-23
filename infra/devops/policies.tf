resource "azuredevops_branch_policy_min_reviewers" "main" {
  project_id = azuredevops_project.project.id

  enabled  = true
  blocking = true

  settings {
    reviewer_count     = 1
    submitter_can_vote = false

    scope {
      repository_id  = data.azuredevops_git_repository.default.id
      repository_ref = data.azuredevops_git_repository.default.default_branch
      match_type     = "Exact"
    }
  }
}

resource "azuredevops_branch_policy_work_item_linking" "main" {
  project_id = azuredevops_project.project.id

  enabled  = true
  blocking = true

  settings {
    scope {
      repository_id  = data.azuredevops_git_repository.default.id
      repository_ref = data.azuredevops_git_repository.default.default_branch
      match_type     = "Exact"
    }
  }
}

resource "azuredevops_branch_policy_comment_resolution" "main" {
  project_id = azuredevops_project.project.id

  enabled  = true
  blocking = true

  settings {
    scope {
      repository_id  = data.azuredevops_git_repository.default.id
      repository_ref = data.azuredevops_git_repository.default.default_branch
      match_type     = "Exact"
    }
  }
}

resource "azuredevops_branch_policy_build_validation" "main" {
  project_id = azuredevops_project.project.id

  enabled  = true
  blocking = true

  settings {
    display_name        = "PR"
    build_definition_id = azuredevops_build_definition.pipelines["PR"].id
    valid_duration      = 720
    filename_patterns   = ["/sources", "/migrations"]

    scope {
      repository_id  = data.azuredevops_git_repository.default.id
      repository_ref = data.azuredevops_git_repository.default.default_branch
      match_type     = "Exact"
    }
  }
}

data "azuredevops_group" "contributors" {
  project_id = azuredevops_project.project.id
  name       = "Contributors"
}

resource "azuredevops_git_permissions" "main" {
  project_id = azuredevops_project.project.id
  principal  = data.azuredevops_group.contributors.id

  permissions = {
    "ForcePush" = "Deny"
    "PolicyExempt" = "Allow"
  }
}
