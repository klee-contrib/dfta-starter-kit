#!/bin/bash
set -e

export PGPASSWORD=$database_admin_secret

function query() {
    psql --host="$database_server" --username="$database_admin_userid" -d $database_name -t -c "$1"
}

echo "Base de données '$database_name' sur '$database_server'"
echo ""

db=$(query 'select "MigrationId" from "__EFMigrationsHistory"' | sed 's/^ *//;/^$/d' | sort -u)

if [[ $db = "" ]]
    then exit 1
fi

ls=$(ls -1 Migrations | sed 's/KleeContribDftaDbContextModelSnapshot//g;s/\.Designer.cs//;s/\.cs//;;/^$/d' | sort -u)

echo "Migrations trouvées en BDD :"
redo_base=0

for script in $db
do
    if echo $ls | grep -qw "$script";
        then 
            echo "- $script => disponible"
        else 
            echo "- $script => manquante"
            redo_base=1
    fi
done

echo ""
if [ $redo_base = 0 ]
    then echo "La mise à jour de la base de données est possible."
    else echo "La mise à jour de la base de données est IMPOSSIBLE."
fi

exit $redo_base