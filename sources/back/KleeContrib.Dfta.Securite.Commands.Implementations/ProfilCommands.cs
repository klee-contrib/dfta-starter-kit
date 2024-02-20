using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Commands.Mutations;
using KleeContrib.Dfta.Securite.Cosmmands;

namespace KleeContrib.Dfta.Securite.Commands.Implementations;

/// <summary>
/// Commandes autour du profil.
/// </summary>
/// <param name="mutations">Service injecté.</param>
[RegisterImpl]
public class ProfilCommands(IProfilMutations mutations) : IProfilCommands
{
    /// <inheritdoc cref="IProfilMutations.AddProfil" />
    public async Task<int> AddProfil(ProfilWrite profil)
    {
        return await mutations.AddProfil(profil);
    }

    /// <inheritdoc cref="IProfilMutations.UpdateProfil" />
    public async Task UpdateProfil(int proId, ProfilWrite profil)
    {
        await mutations.UpdateProfil(proId, profil);
    }
}
