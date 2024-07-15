using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Commands.Mutations;

namespace KleeContrib.Dfta.Securite.Commands.Implementations;

/// <summary>
/// Commandes autour de l'utilisateur.
/// </summary>
[RegisterImpl]
public class UtilisateurCommands(IUtilisateurMutations utilisateurMutations) : IUtilisateurCommands
{
    /// <inheritdoc cref="IUtilisateurCommands.AddUtilisateur" />
    public async Task<int> AddUtilisateur(UtilisateurWrite utilisateur)
    {
        return await utilisateurMutations.AddUtilisateur(utilisateur);
    }

    /// <inheritdoc cref="IUtilisateurCommands.DeleteUtilisateur" />
    public async Task DeleteUtilisateur(int utiId)
    {
        await utilisateurMutations.DeleteUtilisateur(utiId);
    }

    /// <inheritdoc cref="IUtilisateurCommands.UpdateUtilisateur" />
    public async Task UpdateUtilisateur(int utiId, UtilisateurWrite utilisateur)
    {
        await utilisateurMutations.UpdateUtilisateur(utiId, utilisateur);
    }
}
