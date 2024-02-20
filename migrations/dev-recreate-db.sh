source env.sh

echo "Executing recreate-db.sh"
. "recreate-db.sh"

echo "Executing ef database update"
dotnet ef database update
