using Kinetix.Services.Annotations;

namespace KleeContrib.Dfta.Securite.Queries.Implementations;

/// <summary>
/// Implémentation de IUtilisateurMixedQueries.
/// </summary>
[RegisterImpl]
public class UtilisateurMixedQueries(IUtilisateurQueries utilisateurDbQueries, IStorageQueries storageQueries)
    : IUtilisateurMixedQueries
{
    /// <inheritdoc cref="IUtilisateurMixedQueries.GetUtilisateurPhoto" />
    public async Task<byte[]?> GetUtilisateurPhoto(int utiId, CancellationToken ct = default)
    {
        var fileName = await utilisateurDbQueries.GetUtilisateurPhotoFileName(utiId, ct);
        if (fileName == null)
        {
            return null;
        }

        return await storageQueries.GetFile(fileName, ct);
    }
}
