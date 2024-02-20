using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Queries.Models;

namespace KleeContrib.Dfta.Securite.Queries;

[RegisterContract]
public interface IProfilQueries
{
    /// <summary>
    /// Charge le détail d'un Profil
    /// </summary>
    /// <param name="proId">Id technique</param>
    /// <returns>Le détail de l'Profil</returns>
    Task<ProfilRead> GetProfil(int proId);

    /// <summary>
    /// Liste tous les Profils
    /// </summary>
    /// <returns>Profils matchant les critères</returns>
    Task<ICollection<ProfilItem>> GetProfils();
}
