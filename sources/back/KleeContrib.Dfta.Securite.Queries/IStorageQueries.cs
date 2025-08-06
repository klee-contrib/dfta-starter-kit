using Kinetix.Services.Annotations;

namespace KleeContrib.Dfta.Securite.Queries;

/// <summary>
/// Utilisateurs storage queries.
/// </summary>
[RegisterContract]
public interface IStorageQueries
{
    /// <summary>
    /// Récupère un fichier à partir de son nom.
    /// </summary>
    /// <param name="fileName">Nom du fichier.</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Le fichier.</returns>
    public Task<byte[]?> GetFile(string fileName, CancellationToken ct = default);
}
