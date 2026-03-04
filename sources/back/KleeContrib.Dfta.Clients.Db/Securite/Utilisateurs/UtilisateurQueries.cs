using Kinetix.Search.Models;
using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Clients.Db.Securite.Models;
using KleeContrib.Dfta.Securite.Queries;
using KleeContrib.Dfta.Securite.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace KleeContrib.Dfta.Clients.Db.Securite.Utilisateurs;

using static Models.UtilisateurMappers;

/// <summary>
/// Implémentation de IUtilisateurQueries.
/// </summary>
[RegisterImpl]
public class UtilisateurQueries(KleeContribDftaDbContext context) : IUtilisateurQueries
{
    /// <inheritdoc cref="IUtilisateurQueries.GetUtilisateur" />
    public async Task<UtilisateurRead> GetUtilisateur(int utiId, CancellationToken ct = default)
    {
        return CreateUtilisateurRead(
            await context.Utilisateurs.FindAsync([utiId], ct)
                ?? throw new KeyNotFoundException("L'utilisateur demandé n'existe pas.")
        );
    }

    /// <inheritdoc cref="IUtilisateurQueries.GetUtilisateurPhotoFileName" />
    public async Task<string?> GetUtilisateurPhotoFileName(int utiId, CancellationToken ct = default)
    {
        return (await context.Utilisateurs.FindAsync([utiId], ct))?.NomFichierPhoto;
    }

    /// <inheritdoc cref="IUtilisateurQueries.SearchUtilisateur" />
    public async Task<QueryOutput<UtilisateurItem>> SearchUtilisateur(
        QueryInput<UtilisateurCriteria> queryInput,
        CancellationToken ct = default
    )
    {
        var crt = queryInput.Criteria ?? new();

        var query = context
            .Utilisateurs.AsNoTracking()
            .Where(uti =>
                (string.IsNullOrEmpty(crt.Nom) || EF.Functions.ILike(uti.Nom, $"%{crt.Nom}%"))
                && (string.IsNullOrEmpty(crt.Prenom) || EF.Functions.ILike(uti.Nom, $"%{crt.Prenom}%"))
                && (string.IsNullOrEmpty(crt.Email) || EF.Functions.ILike(uti.Nom, $"%{crt.Email}%"))
                && (!crt.DateNaissance.HasValue || uti.DateNaissance == crt.DateNaissance)
                && (!crt.Actif.HasValue || uti.Actif == crt.Actif)
                && (!crt.ProfilId.HasValue || uti.ProfilId == crt.ProfilId)
                && (!crt.TypeUtilisateurCode.HasValue || uti.TypeUtilisateurCode == crt.TypeUtilisateurCode)
            );

        if (queryInput.Sort.Any())
        {
            query = queryInput.Sort[0] switch
            {
                { SortDesc: false, FieldName: var field } => query.OrderBy(uti => EF.Property<Utilisateur>(uti, field)),
                { SortDesc: true, FieldName: var field } => query.OrderByDescending(uti =>
                    EF.Property<Utilisateur>(uti, field)
                ),
            };
        }

        return new QueryOutput<UtilisateurItem>
        {
            List = await query
                .Skip(queryInput.Skip)
                .Take(queryInput.Top ?? 20)
                .Select(x => CreateUtilisateurItem(x))
                .ToListAsync(ct),
            TotalCount = await query.CountAsync(ct),
        };
    }
}
