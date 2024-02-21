#!/bin/sh

# Script to install Azure Pipelines agent

# Download azure agent
runuser -u ${admin_user} -- mkdir /home/${admin_user}/Downloads
runuser -u ${admin_user} -- curl https://vstsagentpackage.azureedge.net/agent/3.234.0/vsts-agent-linux-x64-3.234.0.tar.gz --output /home/${admin_user}/Downloads/vsts-agent-linux-x64-3.234.0.tar.gz
runuser -u ${admin_user} -- mkdir /home/${admin_user}/azure_agent && cd /home/${admin_user}/azure_agent
runuser -u ${admin_user} -- tar zxvf /home/${admin_user}/Downloads/vsts-agent-linux-x64-3.234.0.tar.gz

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
sudo wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo apt-key add -

sudo wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo rm packages-microsoft-prod.deb

sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0
sudo apt-get install -y postgresql-client-16