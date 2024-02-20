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
/// <param name="context">DbContext injecté.</param>
[RegisterImpl]
public class UtilisateurMutations(KleeContribDftaDbContext context) : IUtilisateurMutations
{
    public async Task<int> AddUtilisateur(UtilisateurWrite utilisateur)
    {
        var utilisateurDb = utilisateur.ToUtilisateur();
        await context.Utilisateurs.AddAsync(utilisateurDb);
        await context.SaveChangesAsync();
        return utilisateurDb.Id.Value;
    }

    public async Task DeleteUtilisateur(int utiId)
    {
        var utilisateur = await context.Utilisateurs.FindAsync(utiId);
        context.Remove(utilisateur);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUtilisateur(int utiId, UtilisateurWrite utilisateur)
    {
        var utilisateurDb = await context.Utilisateurs.AsTracking().SingleAsync(x => x.Id == utiId);
        utilisateurDb = utilisateur.ToUtilisateur(utilisateurDb);
        await context.SaveChangesAsync();
    }
}
