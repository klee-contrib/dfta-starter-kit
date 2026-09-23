# Migrations de base de données

Les mises à jour de la base de données sont gérées par Entity Framework, à l'aide de l'outil `dotnet-ef`. Il s'installe avec la commande `dotnet tool install --global dotnet-ef`. Sa documentation est [disponible ici](https://docs.microsoft.com/en-us/ef/core/cli/dotnet).

Le projet C# `KleeContrib.Dfta.Migrations` permet de gérer les migrations séparément de l'application back. Il référence le `DbContext` et est configuré comme une application .NET classique.

Pour créer une migration, on exécute la commande `dotnet ef migrations add XXXXX` depuis le dossier courant. Elle est générée à partir de l'état du modèle après la dernière migration créée (s'il s'agit bien sûr pas de la première). Cet état est conservé dans `Migrations/KleeContribDftaDbContextModelSnapshot.cs`.

Pour appliquer les migrations, on lance le projet de migrations avec `dotnet run` ou depuis Visual Studio. La configuration de la base de données suit le même principe que celle de l'application Web : les paramètres communs sont définis dans `appsettings.json`, et les autres sont fournis sous forme de variables d'environnement, notamment via `launchSettings.json`.

L'exécution des migrations ne consiste pas simplement à les appliquer : l'état cible de la base de données est également vérifié au préalable. Par défaut, si la base contient **des migrations qui ne sont pas disponibles dans l'exécutable de migrations**, la mise à jour est **annulée**. Cela permet d'éviter de laisser la base de données dans un état incohérent. Ce comportement est configurable avec l'option **`--reset`**, qui accepte les valeurs suivantes :

- `true` : supprimer puis recréer la base de données avant toute autre opération. À utiliser en local ou via la pipeline dédiée "Reset DB" ;
- `if-impossible` : supprimer puis recréer la base de données uniquement si la mise à jour est impossible. C'est l'option configurée par défaut en local et pour les déploiements en CI sur l'environnement de dev.

On peut également utiliser l'option `--check` pour effectuer uniquement la vérification.

_Remarque : on n'utilise pas la commande `dotnet ef database update` pour appliquer les migrations, contrairement à ce qui peut être indiqué dans la documentation de la CLI EF._

Comme la base de données pourrait être reconstruite à chaque déploiement, il n'est pas nécessaire de conserver un historique complet des migrations. De plus, pendant le développement d'une fonctionnalité sur une branche dédiée, plusieurs migrations successives ne sont généralement pas nécessaires. La stratégie consiste donc à réinitialiser régulièrement les migrations : dans un premier temps, pour n'en conserver qu'une par branche de feature, puis, après le merge sur `main`, pour regrouper les changements dans une migration initiale (appelée ici `Init`).

Attention à bien conserver la migration initiale `CreateDb` : elle configure notamment le compte de base de données utilisé par l'application.
