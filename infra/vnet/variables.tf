variable "app_name" {
  type     = string
  nullable = false
}

variable "cidr" {
  type     = string
  nullable = false
}

variable "devops_rg_name" {
  type     = string
  nullable = false
}

variable "devops_vnet_id" {
  type     = string
  nullable = false
}

variable "devops_vnet_name" {
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
