# Modélisation

Le projet utilise [TopModel](https://github.com/klee-contrib/topmodel), un outil open-source développé à KLEE en interne, pour décrire le modèle de données. Il se base sur des fichiers YAML avec une extension VSCode et un générateur de code.

## Description

Le modèle de données est divisé en quatre parties :

- La partie "Data", qui décrit le modèle persisté en base de données de l'application.
- La partie "API", qui décrit les APIs exposées par le back au front.
- La partie "Commands", qui décrit les modèles d'update exposées par le back au front.
- La partie "Queries", qui décrit les modèles de récupérations exposées par le back au front.

TopModel met en oeuvre toute une série de fonctionnalités pour pouvoir définir simplement le modèle exposé à partir du modèle persisté (références de classes et propriétés, mappers...)

Le modèle de données est également divisé par module métier, chaque module ayant ses modèles persisté et exposé. Un module peut référencer d'autres modules, mais jamais de manière circulaire.

La [documentation de TopModel](https://klee-contrib.github.io/topmodel) contient toutes les informations nécessaires pour débuter. Il y a un tutoriel qui explique les principaux usages pas à pas ainsi qu'une documentation plus exhaustive des différentes fonctionnalités de modélisation.

## Génération

TopModel est configuré sur le projet pour générer :

- Le modèle persisté dans le back
- Le modèle exposé dans le back et le front
- Le modèle exposé dans le back pour les commands
- Le modèle exposé dans le back pour les queries
- Les APIs côté serveur dans le back
- Les APIs côté client dans le front

(Le modèle des listes de références est bien persisté mais est quand même généré dans le front)

Cela fonctionne avec les "tags" qui sont spécifiés sur les fichiers ("back-queries", "back-commands", "front") qui permette d'expliciter quel générateur (et donc quel applicatif) doit traiter chacun des fichiers.

TopModel est configuré pour générer du C# et du Typescript. La partie SQL est gérée via les migrations d'Entity Framework. Le C# généré pour le modèle persisté contient les éléments nécessaires pour les migrations soient générées correctement (on fait donc du "Code-First" classique, sauf que code en question est lui-même généré).

En C#, on génère :

- Les classes (persistées et non persistées)
- Le DbContext
- Les accesseurs de liste de références
- Les contrôleurs (génération partielle : l'implémentation des différentes méthodes n'est évidemment pas gérée).
- Les mappers

En Typescript, on génère :

- Les "classes" non persistées
- Les types de listes de références
- Les appels d'API
