using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Common.References.Securite;
using KleeContrib.Dfta.Securite.Queries;
using KleeContrib.Dfta.Securite.Queries.Models;
using Microsoft.EntityFrameworkCore;
using static KleeContrib.Dfta.Clients.Db.Securite.Models.SecuriteMappers;

namespace KleeContrib.Dfta.Clients.Db.Securite.Utilisateurs;

/// <summary>
/// Implémentation de IUtilisateurQueries.
/// </summary>
[RegisterImpl]
public class UtilisateurQueries(KleeContribDftaDbContext context) : IUtilisateurQueries
{
    /// <inheritdoc cref="IUtilisateurQueries.GetUtilisateur" />
    public async Task<UtilisateurRead> GetUtilisateur(int utiId)
    {
        return CreateUtilisateurRead(await context.Utilisateurs.FindAsync(utiId) ?? throw new KeyNotFoundException("L'utilisateur demandé n'existe pas."));
    }

    /// <inheritdoc cref="IUtilisateurQueries.SearchUtilisateur" />
    public async Task<ICollection<UtilisateurItem>> SearchUtilisateur(string? nom = null, string? prenom = null, string? email = null, DateOnly? dateNaissance = null, bool? actif = null, int? profilId = null, TypeUtilisateur.Codes? typeUtilisateurCode = null)
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
            .Select(x => CreateUtilisateurItem(x))
            .ToListAsync();
    }
}
