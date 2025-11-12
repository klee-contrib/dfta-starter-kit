#!/bin/sh

# Script to install Azure Pipelines agent

# Download azure agent
runuser -u ${admin_user} -- mkdir /home/${admin_user}/Downloads
runuser -u ${admin_user} -- curl https://download.agent.dev.azure.com/agent/4.258.1/vsts-agent-linux-x64-4.258.1.tar.gz --output /home/${admin_user}/Downloads/vsts-agent-linux-x64-4.258.1.tar.gz
runuser -u ${admin_user} -- mkdir /home/${admin_user}/azure_agent && cd /home/${admin_user}/azure_agent
runuser -u ${admin_user} -- tar zxvf /home/${admin_user}/Downloads/vsts-agent-linux-x64-4.258.1.tar.gz

# Config azure agent
runuser -u ${admin_user} -- ./config.sh --unattended \
  --agent "${agent_name}" \
  --url "${azure_devops_organization_url}" \
  --auth PAT \
  --token "${pat_token}" \
  --pool "${pool_name}" \
  --replace \
  --acceptTeeEula & wait $!

# Install & start service
sudo ./svc.sh install ${admin_user}
sudo ./svc.sh start

# Install dotnet and psql
sudo sh -c 'echo "deb http://apt.postgresql.org/pub/repos/apt $(lsb_release -cs)-pgdg main" > /etc/apt/sources.list.d/pgdg.list'
curl -fsSL https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo gpg --dearmor -o /etc/apt/trusted.gpg.d/postgresql.gpg

sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y postgresql-client-18
curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash