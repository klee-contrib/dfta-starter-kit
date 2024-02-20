using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;

namespace KleeContrib.Dfta.Securite.Cosmmands;

[RegisterContract]
public interface IProfilCommands
{
    /// <summary>
    /// Ajoute un Profil
    /// </summary>
    /// <param name="profil">Profil à sauvegarder</param>
    /// <returns>Profil sauvegardé</returns>
    Task<int> AddProfil(ProfilWrite profil);

    /// <summary>
    /// Sauvegarde un Profil
    /// </summary>
    /// <param name="proId">Id technique</param>
    /// <param name="profil">Profil à sauvegarder</param>
    /// <returns>Profil sauvegardé</returns>
    Task UpdateProfil(int proId, ProfilWrite profil);
}
