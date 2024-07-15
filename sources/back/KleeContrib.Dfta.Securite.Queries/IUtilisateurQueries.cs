using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Common.References.Securite;
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
    /// <returns>Le détail de l'utilisateur</returns>
    Task<UtilisateurRead> GetUtilisateur(int utiId);

    /// <summary>
    /// Recherche des utilisateurs
    /// </summary>
    /// <param name="nom">Nom de l'utilisateur</param>
    /// <param name="prenom">Nom de l'utilisateur</param>
    /// <param name="email">Email de l'utilisateur</param>
    /// <param name="dateNaissance">Age de l'utilisateur</param>
    /// <param name="actif">Si l'utilisateur est actif</param>
    /// <param name="profilId">Profil de l'utilisateur</param>
    /// <param name="typeUtilisateurCode">Type d'utilisateur</param>
    /// <returns>Utilisateurs matchant les critères</returns>
    Task<ICollection<UtilisateurItem>> SearchUtilisateur(string? nom = null, string? prenom = null, string? email = null, DateOnly? dateNaissance = null, bool? actif = null, int? profilId = null, TypeUtilisateur.Codes? typeUtilisateurCode = null);
}
