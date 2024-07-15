using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Clients.Db.Securite.Models;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Commands.Mutations;
using Microsoft.EntityFrameworkCore;
using static KleeContrib.Dfta.Clients.Db.Securite.Models.SecuriteMappers;

namespace KleeContrib.Dfta.Clients.Db.Securite.Utilisateurs;

/// <summary>
/// Utilisateurs mutations.
/// </summary>
[RegisterImpl]
public class UtilisateurMutations(KleeContribDftaDbContext context) : IUtilisateurMutations
{
    /// <inheritdoc cref="IUtilisateurMutations.AddUtilisateur" />
    public async Task<int> AddUtilisateur(UtilisateurWrite utilisateur)
    {
        var utilisateurDb = utilisateur.ToUtilisateur();
        await context.Utilisateurs.AddAsync(utilisateurDb);
        await context.SaveChangesAsync();
        return utilisateurDb.Id;
    }

    /// <inheritdoc cref="IUtilisateurMutations.DeleteUtilisateur" />
    public async Task DeleteUtilisateur(int utiId)
    {
        var utilisateur = await context.Utilisateurs.SingleAsync(uti => uti.Id == utiId);
        context.Remove(utilisateur);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IUtilisateurMutations.UpdateUtilisateur" />
    public async Task UpdateUtilisateur(int utiId, UtilisateurWrite utilisateur)
    {
        var utilisateurDb = await context.Utilisateurs.AsTracking().SingleAsync(x => x.Id == utiId);
        utilisateurDb = utilisateur.ToUtilisateur(utilisateurDb);
        utilisateurDb.DateModification = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
}
