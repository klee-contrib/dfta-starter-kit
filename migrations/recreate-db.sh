#!/bin/bash
set -e

# Recrée la bdd définie dans "env.sh"

export PGPASSWORD=$database_admin_secret

function query() {
    psql --host="$database_server" --username="$database_admin_userid" -d $1 -Atc "$2"
}

echo "Ensuring Users..."
if [[ $database_app_userid == *-local ]]
then
    echo 'Local user'
    query postgres "DO \$\$ BEGIN CREATE USER \"$database_app_userid\" WITH PASSWORD 'local'; EXCEPTION WHEN duplicate_object THEN null; END \$\$"
else
    echo 'Service principal'
    query postgres "DO \$\$ BEGIN PERFORM pgaadauth_create_principal('$database_app_userid', false, false); EXCEPTION WHEN duplicate_object THEN null; END \$\$"
fi

query postgres "GRANT pg_signal_backend TO \"$database_admin_userid\""
echo "Users OK."

echo "Dropping database $database_name..."
query postgres "DROP DATABASE IF EXISTS $database_name WITH (FORCE)"
echo "Dropped $database_name"

echo "Creating database $database_name..."
if [[ $database_app_userid == *-local ]]
then
    query postgres "CREATE DATABASE $database_name"
else
    query postgres "CREATE DATABASE $database_name with owner azure_pg_admin"
fi
echo "Created $database_name"

echo "Fixing default permissions..."
if [[ $database_app_userid == *-local ]]
then
    query $database_name "GRANT USAGE ON SCHEMA public TO \"$database_app_userid\""
    query $database_name "ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO \"$database_app_userid\""
else
    query $database_name "SET ROLE azure_pg_admin; GRANT USAGE ON SCHEMA public TO \"$database_app_userid\""
    query $database_name "SET ROLE azure_pg_admin; ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO \"$database_app_userid\""
fi
echo "Permissions fixed"

echo "Creating extensions..."
query $database_name "CREATE EXTENSION unaccent"
query $database_name "CREATE TEXT SEARCH CONFIGURATION simple_unaccent ( COPY = simple )"
query $database_name "ALTER TEXT SEARCH CONFIGURATION simple_unaccent ALTER MAPPING FOR hword, hword_part, word WITH unaccent, simple"
echo "Extensions created"
