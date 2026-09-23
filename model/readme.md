# Modélisation

Le projet utilise [TopModel](https://github.com/klee-contrib/topmodel) pour décrire le modèle de données dans des fichiers YAML et générer du code C# et TypeScript. Une extension VS Code facilite l'édition de ces fichiers.

## Description

Le modèle de données est divisé en quatre parties :

- La partie "Data", qui décrit le modèle persisté en base de données de l'application.
- La partie "API", qui décrit les APIs exposées par le back au front.
- La partie "Commands", qui décrit les modèles d'écriture utilisés par le back.
- La partie "Queries", qui décrit les modèles de lecture exposés par le back au front.

TopModel permet de définir le modèle exposé à partir du modèle persisté (références de classes et de propriétés, mappers...).

Le modèle de données est également divisé en modules métier, chacun ayant ses modèles persistés et exposés. Un module peut référencer d'autres modules, mais pas de manière circulaire.

La [documentation de TopModel](https://klee-contrib.github.io/topmodel) contient toutes les informations nécessaires pour débuter. Il y a un tutoriel qui explique les principaux usages pas à pas ainsi qu'une documentation plus exhaustive des différentes fonctionnalités de modélisation.

## Génération

TopModel est configuré sur le projet pour générer :

- Le modèle persisté dans le back
- Le modèle exposé dans le back et le front
- Le modèle exposé dans le back pour les commands
- Le modèle exposé dans le back pour les queries
- Les APIs côté serveur dans le back
- Les APIs côté client dans le front

Les listes de références sont persistées dans le back et leurs modèles sont également générés dans le front.

Cela fonctionne avec les tags spécifiés sur les fichiers (`back`, `commands`, `queries`, `api-commands`, `front`), qui déterminent les générateurs C# et TypeScript exécutés selon `topmodel.config`.

TopModel génère du C# et du TypeScript. La partie SQL est gérée par les migrations Entity Framework. Le C# généré pour le modèle persisté fournit les éléments nécessaires à ces migrations : il s'agit d'une approche Code First dont le code du modèle est lui-même généré.

En C#, TopModel génère :

- Les classes (persistées et non persistées)
- Le DbContext
- Les accesseurs de liste de références
- Les contrôleurs (génération partielle ; l'implémentation des méthodes reste à écrire)
- Les mappers

En TypeScript, TopModel génère :

- Les "classes" non persistées
- Les types de listes de références
- Les appels d'API
