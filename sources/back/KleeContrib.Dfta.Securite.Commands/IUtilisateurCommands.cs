using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;

namespace KleeContrib.Dfta.Securite.Commands;

/// <summary>
/// Commandes autour de l'utilisateur.
/// </summary>
[RegisterContract]
public interface IUtilisateurCommands
{
    /// <summary>
    /// Ajoute un utilisateur
    /// </summary>
    /// <param name="utilisateur">Utilisateur à sauvegarder</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Utilisateur sauvegardé</returns>
    Task<int> AddUtilisateur(UtilisateurCommand utilisateur, CancellationToken ct = default);

    /// <summary>
    /// Ajoute une nouvelle photo pour un utilisateur.
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <param name="uploadedFileName">Nom du fichier.</param>
    /// <param name="photo">Photo.</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Task.</returns>
    Task AddUtilisateurPhoto(int utiId, string uploadedFileName, Stream photo, CancellationToken ct = default);

    /// <summary>
    /// Supprime un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Task.</returns>
    Task DeleteUtilisateur(int utiId, CancellationToken ct = default);

    /// <summary>
    /// Supprime la photo d'un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Task.</returns>
    Task DeleteUtilisateurPhoto(int utiId, CancellationToken ct = default);

    /// <summary>
    /// Sauvegarde un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <param name="utilisateur">Utilisateur à sauvegarder</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Utilisateur sauvegardé</returns>
    Task UpdateUtilisateur(int utiId, UtilisateurCommand utilisateur, CancellationToken ct = default);
}
