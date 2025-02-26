using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Commands.Mutations;
using KleeContrib.Dfta.Securite.Queries;

namespace KleeContrib.Dfta.Securite.Commands.Implementations;

/// <summary>
/// Commandes autour de l'utilisateur.
/// </summary>
[RegisterImpl]
public class UtilisateurCommands(IUtilisateurMutations utilisateurMutations, IUtilisateurDbQueries utilisateurDbQueries, IStorageMutations storageMutations) : IUtilisateurCommands
{
    /// <inheritdoc cref="IUtilisateurCommands.AddUtilisateur" />
    public async Task<int> AddUtilisateur(UtilisateurCommand utilisateur)
    {
        return await utilisateurMutations.AddUtilisateur(utilisateur);
    }

    /// <inheritdoc cref="IUtilisateurCommands.AddUtilisateurPhoto" />
    public async Task AddUtilisateurPhoto(int utiId, string uploadedFileName, Stream photo)
    {
        await DeleteUtilisateurPhoto(utiId);
        var fileName = await storageMutations.AddFile($"{utiId}-{uploadedFileName}", photo);
        await utilisateurMutations.UpdateUtilisateurPhotoFileName(utiId, fileName);
    }

    /// <inheritdoc cref="IUtilisateurCommands.DeleteUtilisateur" />
    public async Task DeleteUtilisateur(int utiId)
    {
        await utilisateurMutations.DeleteUtilisateur(utiId);
    }

    /// <inheritdoc cref="IUtilisateurCommands.DeleteUtilisateurPhoto" />
    public async Task DeleteUtilisateurPhoto(int utiId)
    {
        var fileName = await utilisateurDbQueries.GetUtilisateurPhotoFileName(utiId);
        if (fileName == null)
        {
            return;
        }

        await storageMutations.DeleteFile(fileName);
        await utilisateurMutations.UpdateUtilisateurPhotoFileName(utiId, null);
    }

    /// <inheritdoc cref="IUtilisateurCommands.UpdateUtilisateur" />
    public async Task UpdateUtilisateur(int utiId, UtilisateurCommand utilisateur)
    {
        await utilisateurMutations.UpdateUtilisateur(utiId, utilisateur);
    }
}
