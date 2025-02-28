using Kinetix.Services.Annotations;

namespace KleeContrib.Dfta.Securite.Queries.Implementations;

/// <summary>
/// Implémentation de IUtilisateurMixedQueries.
/// </summary>
[RegisterImpl]
public class UtilisateurMixedQueries(IUtilisateurQueries utilisateurDbQueries, IStorageQueries storageQueries) : IUtilisateurMixedQueries
{
    /// <inheritdoc cref="IUtilisateurMixedQueries.GetUtilisateurPhoto" />
    public async Task<byte[]?> GetUtilisateurPhoto(int utiId)
    {
        var fileName = await utilisateurDbQueries.GetUtilisateurPhotoFileName(utiId);
        if (fileName == null)
        {
            return null;
        }

        return await storageQueries.GetFile(fileName);
    }
}
