////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Securite.Commands.Models;

namespace KleeContrib.Dfta.Api.Models.Commands.Securite;

/// <summary>
/// Mappers pour le module 'Securite.Profil'.
/// </summary>
public static class ProfilMappers
{
    /// <summary>
    /// Mappe 'ProfilWrite' vers 'ProfilCommand'.
    /// </summary>
    /// <param name="source">Instance de 'ProfilWrite'.</param>
    /// <returns>Une nouvelle instance de 'ProfilCommand'.</returns>
    public static ProfilCommand ToProfilCommand(this ProfilWrite source)
    {
        ArgumentNullException.ThrowIfNull(source.Libelle);
        ArgumentNullException.ThrowIfNull(source.DroitCodes);

        return new ProfilCommand
        {
            Libelle = source.Libelle,
            DroitCodes = source.DroitCodes
        };
    }

    /// <summary>
    /// Mappe 'ProfilWrite' vers 'ProfilCommand'.
    /// </summary>
    /// <param name="source">Instance de 'ProfilWrite'.</param>
    /// <param name="dest">Instance pré-existante de 'ProfilCommand'.</param>
    /// <returns>L'instance pré-existante de 'ProfilCommand'.</returns>
    public static ProfilCommand ToProfilCommand(this ProfilWrite source, ProfilCommand dest)
    {
        ArgumentNullException.ThrowIfNull(source.Libelle);
        ArgumentNullException.ThrowIfNull(source.DroitCodes);

        dest.Libelle = source.Libelle;
        dest.DroitCodes = source.DroitCodes;
        return dest;
    }
}
