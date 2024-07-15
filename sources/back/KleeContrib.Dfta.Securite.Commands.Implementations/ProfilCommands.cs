using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Commands.Mutations;
using KleeContrib.Dfta.Securite.Cosmmands;

namespace KleeContrib.Dfta.Securite.Commands.Implementations;

/// <summary>
/// Implémentation de IProfilCommands.
/// </summary>
[RegisterImpl]
public class ProfilCommands(IProfilMutations mutations) : IProfilCommands
{
    /// <inheritdoc cref="IProfilCommands.AddProfil" />
    public async Task<int> AddProfil(ProfilWrite profil)
    {
        return await mutations.AddProfil(profil);
    }

    /// <inheritdoc cref="IProfilCommands.UpdateProfil" />
    public async Task UpdateProfil(int proId, ProfilWrite profil)
    {
        await mutations.UpdateProfil(proId, profil);
    }
}
