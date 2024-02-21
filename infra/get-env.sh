# Récupère les infos nécessaires pour un environnement local depuis le state terraform.
# Les 4 valeurs récupérées ne sont pas des secrets et pourraient être commitées, mais ce mécanisme pourrait être étendu pour en ajouter.

echo "COMPOSE_PROJECT_NAME='kleecontrib-dfta'" > ../sources/.env

state=$(terraform show -json)

tenant_id=$(echo $state | jq -r '.values.root_module.child_modules[] | select(.address == "module.aad") | .resources[] | select(.address == "module.aad.data.azuread_client_config.current") | .values.tenant_id')
front_client_id=$(echo $state | jq -r '.values.root_module.child_modules[] | select(.address == "module.aad") | .resources[] | select(.address == "module.aad.azuread_application.front") | .values.client_id')
back_client_id=$(echo $state | jq -r '.values.root_module.child_modules[] | select(.address == "module.aad") | .resources[] | select(.address == "module.aad.azuread_application.back") | .values.client_id')
audience=$(echo $state | jq -r '.values.root_module.child_modules[] | select(.address == "module.aad") | .resources[] | select(.address == "module.aad.azuread_application.back") | .values.identifier_uris[0]')

echo "tenant_id='$tenant_id'" >> ../sources/.env
echo "front_client_id='$front_client_id'" >> ../sources/.env
echo "back_client_id='$back_client_id'" >> ../sources/.env
echo "audience='$audience'" >> ../sources/.env

mkdir -p ../sources/front/public
echo "{\"environment\": \"dev\", \"clientId\": \""$front_client_id"\", \"tenantId\": \""$tenant_id"\", \"audience\": \""$audience"\"}" > ../sources/front/public/config.json