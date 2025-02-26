using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Common.References.Securite;
using KleeContrib.Dfta.Securite.Queries;
using KleeContrib.Dfta.Securite.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace KleeContrib.Dfta.Clients.Db.Securite.Utilisateurs;

using static Models.SecuriteMappers;

/// <summary>
/// Implémentation de IUtilisateurQueries.
/// </summary>
[RegisterImpl]
public class UtilisateurDbQueries(KleeContribDftaDbContext context) : IUtilisateurDbQueries
{
    /// <inheritdoc cref="IUtilisateurDbQueries.GetUtilisateur" />
    public async Task<UtilisateurQuery> GetUtilisateur(int utiId)
    {
        return CreateUtilisateurQuery(await context.Utilisateurs.FindAsync(utiId) ?? throw new KeyNotFoundException("L'utilisateur demandé n'existe pas."));
    }

    /// <inheritdoc cref="IUtilisateurDbQueries.GetUtilisateurPhotoFileName" />
    public async Task<string?> GetUtilisateurPhotoFileName(int utiId)
    {
        return (await context.Utilisateurs.FindAsync(utiId))?.NomFichierPhoto;
    }

    /// <inheritdoc cref="IUtilisateurDbQueries.SearchUtilisateur" />
    public async Task<ICollection<UtilisateurItemQuery>> SearchUtilisateur(string? nom = null, string? prenom = null, string? email = null, DateOnly? dateNaissance = null, bool? actif = null, int? profilId = null, TypeUtilisateur.Codes? typeUtilisateurCode = null)
    {
        return await context.Utilisateurs.AsNoTracking()
            .Where(x =>
                (string.IsNullOrEmpty(nom) || x.Nom == nom) &&
                (string.IsNullOrEmpty(prenom) || x.Prenom == prenom) &&
                (string.IsNullOrEmpty(email) || x.Email == email) &&
                (!dateNaissance.HasValue || x.DateNaissance == dateNaissance) &&
                (!actif.HasValue || x.Actif == actif) &&
                (!profilId.HasValue || x.ProfilId == profilId) &&
                (!typeUtilisateurCode.HasValue || x.TypeUtilisateurCode == typeUtilisateurCode))
            .Select(x => CreateUtilisateurItemQuery(x))
            .ToListAsync();
    }
}
