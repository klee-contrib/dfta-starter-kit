using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Commands.Mutations;
using Microsoft.EntityFrameworkCore;

namespace KleeContrib.Dfta.Clients.Db.Securite.Utilisateurs;

using static Models.UtilisateurMappers;

/// <summary>
/// Utilisateurs mutations.
/// </summary>
[RegisterImpl]
public class UtilisateurMutations(KleeContribDftaDbContext context) : IUtilisateurMutations
{
    /// <inheritdoc cref="IUtilisateurMutations.AddUtilisateur" />
    public async Task<int> AddUtilisateur(UtilisateurCommand utilisateur)
    {
        var utilisateurDb = utilisateur.ToUtilisateur();
        await context.Utilisateurs.AddAsync(utilisateurDb);
        await context.SaveChangesAsync();
        return utilisateurDb.Id;
    }

    /// <inheritdoc cref="IUtilisateurMutations.DeleteUtilisateur" />
    public async Task DeleteUtilisateur(int utiId)
    {
        await context.Utilisateurs.Where(uti => uti.Id == utiId).ExecuteDeleteAsync();
    }

    /// <inheritdoc cref="IUtilisateurMutations.UpdateUtilisateur" />
    public async Task UpdateUtilisateur(int utiId, UtilisateurCommand utilisateur)
    {
        var utilisateurDb = await context.Utilisateurs.FindAsync(utiId) ?? throw new KeyNotFoundException("L'utilisateur demandé n'existe pas.");

        utilisateur.ToUtilisateur(utilisateurDb);
        utilisateurDb.DateModification = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IUtilisateurMutations.UpdateUtilisateurPhotoFileName" />
    public async Task UpdateUtilisateurPhotoFileName(int utiId, string? fileName)
    {
        var utilisateurDb = await context.Utilisateurs.FindAsync(utiId) ?? throw new KeyNotFoundException("L'utilisateur demandé n'existe pas.");

        utilisateurDb.NomFichierPhoto = fileName;
        utilisateurDb.DateModification = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }
}
