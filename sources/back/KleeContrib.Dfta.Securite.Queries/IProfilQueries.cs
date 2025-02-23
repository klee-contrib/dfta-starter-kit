using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Queries.Models;

namespace KleeContrib.Dfta.Securite.Queries;

/// <summary>
/// Profils queries.
/// </summary>
[RegisterContract]
public interface IProfilQueries
{
    /// <summary>
    /// Charge le détail d'un Profil
    /// </summary>
    /// <param name="proId">Id technique</param>
    /// <returns>Le détail de l'Profil</returns>
    Task<ProfilQuery> GetProfil(int proId);

    /// <summary>
    /// Liste tous les Profils
    /// </summary>
    /// <returns>Profils matchant les critères</returns>
    Task<ICollection<ProfilItemQuery>> GetProfils();
}
