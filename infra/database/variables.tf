variable "app_name" {
  type     = string
  nullable = false
}

variable "devops_service_connection_spn" {
  type     = string
  nullable = false
}

variable "pg_version" {
  type     = number
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

variable "storage_mb" {
  type     = number
  nullable = false
}

variable "sku_name" {
  type     = string
  nullable = false
}

variable "snet_id" {
  type     = string
  nullable = false
}

variable "vault_id" {
  type     = string
  nullable = false
}

variable "vnet_id" {
  type     = string
  nullable = false
}

variable "zone" {
  type     = number
  nullable = false
}
