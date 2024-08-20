using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Clients.Db.Securite.Models;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Commands.Mutations;
using Microsoft.EntityFrameworkCore;
using static KleeContrib.Dfta.Clients.Db.Securite.Models.SecuriteMappers;

namespace KleeContrib.Dfta.Clients.Db.Securite.Profils;

/// <summary>
/// Implémentation de IProfilMutations.
/// </summary>
[RegisterImpl]
public class ProfilMutations(KleeContribDftaDbContext context) : IProfilMutations
{
    /// <inheritdoc cref="IProfilMutations.AddProfil" />
    public async Task<int> AddProfil(ProfilWrite profil)
    {
        var profilDb = profil.ToProfil();

        await context.Profils.AddAsync(profilDb);
        await context.SaveChangesAsync();

        await context.DroitProfils.AddRangeAsync((profil.DroitCodes ?? [])
            .Select(droitCode => new DroitProfil
            {
                DroitCode = droitCode,
                ProfilId = profilDb.Id
            }));

        await context.SaveChangesAsync();

        return profilDb.Id;
    }

    /// <inheritdoc cref="IProfilMutations.UpdateProfil" />
    public async Task UpdateProfil(int proId, ProfilWrite profil)
    {
        var profilDb = await context.Profils.FindAsync(proId) ?? throw new KeyNotFoundException("Le profil demandé n'existe pas.");

        profil.ToProfil(profilDb);
        profilDb.DateModification = DateTime.UtcNow;

        await context.DroitProfils.Where(x => x.ProfilId == proId).ExecuteDeleteAsync();
        await context.DroitProfils.AddRangeAsync((profil.DroitCodes ?? []).Select(x => new DroitProfil
        {
            DroitCode = x,
            ProfilId = profilDb.Id
        }));

        await context.SaveChangesAsync();
    }
}
