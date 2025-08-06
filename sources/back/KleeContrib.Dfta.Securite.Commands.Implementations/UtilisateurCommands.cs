using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Commands.Mutations;
using KleeContrib.Dfta.Securite.Queries;

namespace KleeContrib.Dfta.Securite.Commands.Implementations;

/// <summary>
/// Commandes autour de l'utilisateur.
/// </summary>
[RegisterImpl]
public class UtilisateurCommands(IUtilisateurMutations utilisateurMutations, IUtilisateurQueries utilisateurDbQueries, IStorageMutations storageMutations) : IUtilisateurCommands
{
    /// <inheritdoc cref="IUtilisateurCommands.AddUtilisateur" />
    public async Task<int> AddUtilisateur(UtilisateurCommand utilisateur, CancellationToken ct = default)
    {
        return await utilisateurMutations.AddUtilisateur(utilisateur, ct);
    }

    /// <inheritdoc cref="IUtilisateurCommands.AddUtilisateurPhoto" />
    public async Task AddUtilisateurPhoto(int utiId, string uploadedFileName, Stream photo, CancellationToken ct = default)
    {
        await DeleteUtilisateurPhoto(utiId, ct);
        var fileName = await storageMutations.AddFile($"{utiId}-{uploadedFileName}", photo, ct);
        await utilisateurMutations.UpdateUtilisateurPhotoFileName(utiId, fileName, ct);
    }

    /// <inheritdoc cref="IUtilisateurCommands.DeleteUtilisateur" />
    public async Task DeleteUtilisateur(int utiId, CancellationToken ct = default)
    {
        await utilisateurMutations.DeleteUtilisateur(utiId, ct);
    }

    /// <inheritdoc cref="IUtilisateurCommands.DeleteUtilisateurPhoto" />
    public async Task DeleteUtilisateurPhoto(int utiId, CancellationToken ct = default)
    {
        var fileName = await utilisateurDbQueries.GetUtilisateurPhotoFileName(utiId, ct);
        if (fileName == null)
        {
            return;
        }

        await storageMutations.DeleteFile(fileName, ct);
        await utilisateurMutations.UpdateUtilisateurPhotoFileName(utiId, null, ct);
    }

    /// <inheritdoc cref="IUtilisateurCommands.UpdateUtilisateur" />
    public async Task UpdateUtilisateur(int utiId, UtilisateurCommand utilisateur, CancellationToken ct = default)
    {
        await utilisateurMutations.UpdateUtilisateur(utiId, utilisateur, ct);
    }
}
