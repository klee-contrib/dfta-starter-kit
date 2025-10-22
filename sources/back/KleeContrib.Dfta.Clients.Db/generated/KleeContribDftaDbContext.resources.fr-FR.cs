////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Clients.Db.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace KleeContrib.Dfta.Clients.Db;

/// <summary>
/// Partial pour ajouter les traductions dans EF.
/// </summary>
public partial class KleeContribDftaDbContext : DbContext
{
    partial void AddFrFRResources(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Traduction>().HasData(
            new Traduction { ResourceKey = "securite.droit.values.Create", Locale = "fr-FR", Label = "Création" },
            new Traduction { ResourceKey = "securite.droit.values.Delete", Locale = "fr-FR", Label = "Suppression" },
            new Traduction { ResourceKey = "securite.droit.values.Read", Locale = "fr-FR", Label = "Lecture" },
            new Traduction { ResourceKey = "securite.droit.values.Update", Locale = "fr-FR", Label = "Mise à jour" },
            new Traduction { ResourceKey = "securite.typeDroit.values.Admin", Locale = "fr-FR", Label = "Administration" },
            new Traduction { ResourceKey = "securite.typeDroit.values.Read", Locale = "fr-FR", Label = "Lecture" },
            new Traduction { ResourceKey = "securite.typeDroit.values.Write", Locale = "fr-FR", Label = "Ecriture" },
            new Traduction { ResourceKey = "securite.typeUtilisateur.values.Admin", Locale = "fr-FR", Label = "Administrateur" },
            new Traduction { ResourceKey = "securite.typeUtilisateur.values.Client", Locale = "fr-FR", Label = "Client" },
            new Traduction { ResourceKey = "securite.typeUtilisateur.values.Gestionnaire", Locale = "fr-FR", Label = "Gestionnaire" }
        );
    }
}
