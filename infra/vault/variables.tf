variable "app_name" {
  type     = string
  nullable = false
}

variable "devops_service_connection_spn" {
  type     = string
  nullable = false
}

variable "region" {
  type     = string
  nullable = false
}

variable "rg_name" {
  type     = string
  nullable = false
}
