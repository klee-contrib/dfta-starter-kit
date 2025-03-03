# Migrations de base de données

Les mises à jour de base de données sont gérées par Entity Framework, via l'utilitaire `dotnet-ef` (qui s'installe via la commande `dotnet tool install --global dotnet-ef`).

Le projet C# courant (`KleeContrib.Dfta.Migrations`) a pour but de gérer ces migrations séparément de l'applicatif back. Il référence le DbContext et l'utilise pour générer les migrations.

La commande `dotnet ef migrations add XXXXX` permet de créer une nouvelle migration à partir de l'état de la base de données après la dernière migration générée (conservé dans `Migrations/KleeContribDftaDbContextModelSnapshot.cs`). La commande `dotnet ef database update` permet ensuite de jouer cette migration en base de données.

L'exécutable est configuré pour récupérer les infos de connexion depuis les variables d'environnement. Pour le dev local, elles sont toutes définies dans le fichier `env.sh`. Pour l'utiliser, dans la console Git Bash, il faut faire un `source env.sh` avant de lancer les commandes `dotnet ef`.

Il y a aussi 2 autres scripts bash :

- `recreate-db.sh` qui, comme son nom l'indique, permet de supprimer et de recréer la base de données. Il est lancé (pour l'instant) à chaque déploiement dans un environnement Azure.
- `dev-recreate-db.sh` qui exécute dans l'ordre `recreate-db.sh` puis `dotnet ef database update`.

Dans l'idée, puisqu'on reconstruit la base à chaque déploiement, on a pas envie de gérer tout un historique de migrations alors qu'on n'en a pas besoin. De plus, lors du développement d'une fonctionnalité sur une branche dédiée, il n'y aura jamais besoin de faire plusieurs migrations successives. La stratégie employée sera donc de réinitialiser les migrations régulièrement pour n'en conserver d'abord une par branche de fonctionnalité, puis une fois mergée sur `main` une autre réinitialisation sera faite pour la fusionner avec la migration initiale.

La commande `dotnet ef migrations remove` permet de supprimer la dernière migration. S'il est possible de demander à EF de jouer les migrations à l'envers, le plus simple reste quand même de réinitialiser la base de données lorsqu'il y a besoin de faire ça (d'où le script dédié).

La doc complète sur `dotnet-ef` est [disponible ici](https://docs.microsoft.com/en-us/ef/core/cli/dotnet).
