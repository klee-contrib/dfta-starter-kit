using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;

namespace KleeContrib.Dfta.Securite.Commands;

/// <summary>
/// Commandes autour du profil.
/// </summary>
[RegisterContract]
public interface IProfilCommands
{
    /// <summary>
    /// Ajoute un Profil
    /// </summary>
    /// <param name="profil">Profil à sauvegarder</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Profil sauvegardé</returns>
    Task<int> AddProfil(ProfilCommand profil, CancellationToken ct = default);

    /// <summary>
    /// Sauvegarde un Profil
    /// </summary>
    /// <param name="proId">Id technique</param>
    /// <param name="profil">Profil à sauvegarder</param>
    /// <param name="ct">CancellationToken.</param>
    /// <returns>Profil sauvegardé</returns>
    Task UpdateProfil(int proId, ProfilCommand profil, CancellationToken ct = default);
}
