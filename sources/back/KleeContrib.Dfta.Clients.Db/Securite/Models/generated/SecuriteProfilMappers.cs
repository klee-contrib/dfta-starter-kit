////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Securite.Commands.Models;

namespace KleeContrib.Dfta.Clients.Db.Securite.Models;

/// <summary>
/// Mappers pour le module 'Securite.Profil'.
/// </summary>
public static class SecuriteProfilMappers
{
    /// <summary>
    /// Mappe 'Profil' vers 'Profil'.
    /// </summary>
    /// <param name="source">Instance de 'Profil'.</param>
    /// <returns>Une nouvelle instance de 'Profil'.</returns>
    public static Profil ToProfil(this Profil source)
    {
        return new Profil
        {
            Libelle = source.Libelle
        };
    }

    /// <summary>
    /// Mappe 'Profil' vers 'Profil'.
    /// </summary>
    /// <param name="source">Instance de 'Profil'.</param>
    /// <param name="dest">Instance pré-existante de 'Profil'.</param>
    /// <returns>L'instance pré-existante de 'Profil'.</returns>
    public static Profil ToProfil(this Profil source, Profil dest)
    {
        dest.Libelle = source.Libelle;
        return dest;
    }

    /// <summary>
    /// Mappe 'ProfilCommand' vers 'Profil'.
    /// </summary>
    /// <param name="source">Instance de 'ProfilCommand'.</param>
    /// <returns>Une nouvelle instance de 'Profil'.</returns>
    public static Profil ToProfil(this ProfilCommand source)
    {
        return new Profil
        {
            Libelle = source.Libelle
        };
    }

    /// <summary>
    /// Mappe 'ProfilCommand' vers 'Profil'.
    /// </summary>
    /// <param name="source">Instance de 'ProfilCommand'.</param>
    /// <param name="dest">Instance pré-existante de 'Profil'.</param>
    /// <returns>L'instance pré-existante de 'Profil'.</returns>
    public static Profil ToProfil(this ProfilCommand source, Profil dest)
    {
        dest.Libelle = source.Libelle;
        return dest;
    }
}
