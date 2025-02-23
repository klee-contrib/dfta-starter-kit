using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Queries;
using KleeContrib.Dfta.Securite.Queries.Models;
using Microsoft.EntityFrameworkCore;

namespace KleeContrib.Dfta.Clients.Db.Securite.Profils;

using static Models.SecuriteMappers;

/// <summary>
/// Profils queries.
/// </summary>
[RegisterImpl]
public class ProfilQueries(KleeContribDftaDbContext context) : IProfilQueries
{
    /// <inheritdoc cref="IProfilQueries.GetProfil" />
    public async Task<ProfilQuery> GetProfil(int proId)
    {
        return await (
            from profil in context.Profils
            where profil.Id == proId
            select new ProfilQuery
            {
                DroitCodes = context.DroitProfils.Where(x => x.ProfilId == proId).Select(d => d.DroitCode).ToArray(),
                Id = profil.Id,
                Libelle = profil.Libelle,
                Utilisateurs = context.Utilisateurs.Where(x => x.ProfilId == proId).Select(x => CreateUtilisateurItemQuery(x)).ToList(),
                DateCreation = profil.DateCreation,
                DateModification = profil.DateModification
            }).SingleOrDefaultAsync() ?? throw new KeyNotFoundException("Le profil demandé n'existe pas.");
    }

    /// <inheritdoc cref="IProfilQueries.GetProfils" />
    public async Task<ICollection<ProfilItemQuery>> GetProfils()
    {
        return await (
            from profil in context.Profils.AsNoTracking()
            select new ProfilItemQuery
            {
                NombreUtilisateurs = context.Utilisateurs.Where(x => x.ProfilId == profil.Id).Count(),
                Id = profil.Id,
                Libelle = profil.Libelle
            }).ToListAsync();
    }
}
