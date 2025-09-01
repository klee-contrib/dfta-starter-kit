using Kinetix.Services.Annotations;

namespace KleeContrib.Dfta.Securite.Queries;

/// <summary>
/// Queries utilisateur qui utilisent plusieurs clients.
/// </summary>
[RegisterContract]
public interface IUtilisateurMixedQueries
{
    /// <summary>
    /// Charge la photo d'un utilisateur (si elle existe).
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Photo.</returns>
    Task<byte[]?> GetUtilisateurPhoto(int utiId, CancellationToken ct = default);
}
