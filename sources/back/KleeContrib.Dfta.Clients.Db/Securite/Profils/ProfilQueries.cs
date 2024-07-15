using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Queries;
using KleeContrib.Dfta.Securite.Queries.Models;
using Microsoft.EntityFrameworkCore;
using static KleeContrib.Dfta.Clients.Db.Securite.Models.SecuriteMappers;

namespace KleeContrib.Dfta.Clients.Db.Securite.Profils;

/// <summary>
/// Profils queries.
/// </summary>
[RegisterImpl]
public class ProfilQueries(KleeContribDftaDbContext context) : IProfilQueries
{
    /// <inheritdoc cref="IProfilQueries.GetProfil" />
    public async Task<ProfilRead> GetProfil(int proId)
    {
        return await (
            from profil in context.Profils
            where profil.Id == proId
            select new ProfilRead
            {
                DroitCodes = context.DroitProfils.Where(x => x.ProfilId == proId).Select(d => d.DroitCode).ToArray(),
                Id = profil.Id,
                Libelle = profil.Libelle,
                Utilisateurs = context.Utilisateurs.Where(x => x.ProfilId == proId).Select(x => CreateUtilisateurItem(x)).ToList(),
                DateCreation = profil.DateCreation,
                DateModification = profil.DateModification
            }).SingleAsync();
    }

    /// <inheritdoc cref="IProfilQueries.GetProfils" />
    public async Task<ICollection<ProfilItem>> GetProfils()
    {
        return await (
            from profil in context.Profils
            select new ProfilItem
            {
                NombreUtilisateurs = context.Utilisateurs.Where(x => x.ProfilId == profil.Id).Count(),
                Id = profil.Id,
                Libelle = profil.Libelle
            }).ToListAsync();
    }
}
