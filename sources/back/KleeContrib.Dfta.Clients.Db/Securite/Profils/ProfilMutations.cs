using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Clients.Db.Securite.Models;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Commands.Mutations;
using Microsoft.EntityFrameworkCore;
using static KleeContrib.Dfta.Clients.Db.Securite.Models.SecuriteMappers;

namespace KleeContrib.Dfta.Clients.Db.Securite.Profils;

/// <summary>
/// Profils mutations.
/// </summary>
/// <param name="context">DbContext injecté.</param>
[RegisterImpl]
public class ProfilMutations(KleeContribDftaDbContext context) : IProfilMutations
{
    /// <inheritdoc cref="IProfilMutations.AddProfil" />
    public async Task<int> AddProfil(ProfilWrite profil)
    {
        var profilDb = profil.ToProfil();

        await context.Profils.AddAsync(profilDb);
        await context.SaveChangesAsync();

        await context.DroitProfils.AddRangeAsync(profil.DroitCodes.Select(x => new DroitProfil
        {
            DroitCode = x,
            ProfilId = profilDb.Id
        }));

        await context.SaveChangesAsync();

        return profilDb.Id.Value;
    }

    /// <inheritdoc cref="IProfilMutations.UpdateProfil" />
    public async Task UpdateProfil(int proId, ProfilWrite profil)
    {
        var profilDb = await context.Profils.AsTracking().SingleAsync(x => x.Id == proId);
        profilDb = profil.ToProfil(profilDb);

        context.DroitProfils.RemoveRange(context.DroitProfils.Where(x => x.ProfilId == proId));

        await context.DroitProfils.AddRangeAsync(profil.DroitCodes.Select(x => new DroitProfil
        {
            DroitCode = x,
            ProfilId = profilDb.Id
        }));

        await context.SaveChangesAsync();
    }
}
