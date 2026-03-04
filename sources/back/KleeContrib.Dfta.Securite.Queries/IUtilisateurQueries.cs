using Kinetix.Search.Models;
using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Queries.Models;

namespace KleeContrib.Dfta.Securite.Queries;

/// <summary>
/// Utilisateurs queries.
/// </summary>
[RegisterContract]
public interface IUtilisateurQueries
{
    /// <summary>
    /// Charge le détail d'un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Le détail de l'utilisateur</returns>
    Task<UtilisateurRead> GetUtilisateur(int utiId, CancellationToken ct = default);

    /// <summary>
    /// Charge le nom du fichier de photo de l'utilisateur.
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur.</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Nom du fichier de photo, s'il existe.</returns>
    Task<string?> GetUtilisateurPhotoFileName(int utiId, CancellationToken ct = default);

    /// <summary>
    /// Recherche des utilisateurs
    /// </summary>
    /// <param name="queryInput">Critères de recherche</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Utilisateurs matchant les critères</returns>
    Task<QueryOutput<UtilisateurItem>> SearchUtilisateur(
        QueryInput<UtilisateurCriteria> queryInput,
        CancellationToken ct = default
    );
}
