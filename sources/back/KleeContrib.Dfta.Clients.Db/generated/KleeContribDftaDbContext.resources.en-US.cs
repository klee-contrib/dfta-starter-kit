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
    partial void AddEnUSResources(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Traduction>().HasData(
            new Traduction { ResourceKey = "securite.droit.values.Create", Locale = "en-US", Label = "Create" },
            new Traduction { ResourceKey = "securite.droit.values.Delete", Locale = "en-US", Label = "Delete" },
            new Traduction { ResourceKey = "securite.droit.values.Read", Locale = "en-US", Label = "Read" },
            new Traduction { ResourceKey = "securite.droit.values.Update", Locale = "en-US", Label = "Update" },
            new Traduction { ResourceKey = "securite.typeDroit.values.Admin", Locale = "en-US", Label = "Admin" },
            new Traduction { ResourceKey = "securite.typeDroit.values.Read", Locale = "en-US", Label = "Read" },
            new Traduction { ResourceKey = "securite.typeDroit.values.Write", Locale = "en-US", Label = "Write" },
            new Traduction { ResourceKey = "securite.typeUtilisateur.values.Admin", Locale = "en-US", Label = "Administrator" },
            new Traduction { ResourceKey = "securite.typeUtilisateur.values.Client", Locale = "en-US", Label = "Client" },
            new Traduction { ResourceKey = "securite.typeUtilisateur.values.Gestionnaire", Locale = "en-US", Label = "Manager" }
        );
    }
}
