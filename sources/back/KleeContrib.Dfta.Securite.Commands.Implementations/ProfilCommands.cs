using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Commands.Mutations;

namespace KleeContrib.Dfta.Securite.Commands.Implementations;

/// <summary>
/// Implémentation de IProfilCommands.
/// </summary>
[RegisterImpl]
public class ProfilCommands(IProfilMutations mutations) : IProfilCommands
{
    /// <inheritdoc cref="IProfilCommands.AddProfil" />
    public async Task<int> AddProfil(ProfilCommand profil, CancellationToken ct = default)
    {
        return await mutations.AddProfil(profil, ct);
    }

    /// <inheritdoc cref="IProfilCommands.UpdateProfil" />
    public async Task UpdateProfil(int proId, ProfilCommand profil, CancellationToken ct = default)
    {
        await mutations.UpdateProfil(proId, profil, ct);
    }
}
