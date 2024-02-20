#!/bin/bash
set -e

# Recrée la bdd définie dans "env.sh"

export PGPASSWORD=$database_admin_secret

function query() {
    psql --host=$database_server --username=$database_admin_userid -d $1 -Atc "$2"
}

echo "Ensuring Users..."
query postgres "DO \$\$ BEGIN CREATE USER dfta_app WITH PASSWORD '$database_dfta_app_secret'; EXCEPTION WHEN duplicate_object THEN ALTER ROLE dfta_app WITH PASSWORD '$database_dfta_app_secret'; END \$\$"
query postgres "DO \$\$ BEGIN CREATE USER dfta_read WITH PASSWORD '$database_dfta_read_secret'; EXCEPTION WHEN duplicate_object THEN ALTER ROLE dfta_read WITH PASSWORD '$database_dfta_read_secret'; END \$\$"
query postgres "GRANT pg_signal_backend TO $database_admin_userid"
echo "Users OK."

echo "Dropping database $database_name..."
query postgres "DROP DATABASE IF EXISTS $database_name WITH (FORCE)"
echo "Dropped $database_name"

echo "Creating database $database_name..."
query postgres "CREATE DATABASE $database_name"
echo "Created $database_name"

echo "Fixing default permissions..."
query $database_name "ALTER DEFAULT PRIVILEGES GRANT USAGE ON SCHEMAS TO dfta_app"
query $database_name "ALTER DEFAULT PRIVILEGES GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO dfta_app"
query $database_name "ALTER DEFAULT PRIVILEGES GRANT USAGE ON SCHEMAS TO dfta_read"
query $database_name "ALTER DEFAULT PRIVILEGES GRANT SELECT ON TABLES TO dfta_read"
echo "Permissions fixed"

echo "Creating extensions..."
query $database_name "CREATE EXTENSION unaccent"
query $database_name "CREATE TEXT SEARCH CONFIGURATION simple_unaccent ( COPY = simple )"
query $database_name "ALTER TEXT SEARCH CONFIGURATION simple_unaccent ALTER MAPPING FOR hword, hword_part, word WITH unaccent, simple"
echo "Extensions created"

