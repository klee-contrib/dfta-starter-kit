////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Securite.Commands.Models;

namespace KleeContrib.Dfta.Api.Models.Commands;

/// <summary>
/// Mappers pour le module 'Securite'.
/// </summary>
public static class SecuriteDTOMappers
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

    /// <summary>
    /// Mappe 'UtilisateurWrite' vers 'UtilisateurCommand'.
    /// </summary>
    /// <param name="source">Instance de 'UtilisateurWrite'.</param>
    /// <returns>Une nouvelle instance de 'UtilisateurCommand'.</returns>
    public static UtilisateurCommand ToUtilisateurCommand(this UtilisateurWrite source)
    {
        ArgumentNullException.ThrowIfNull(source.Nom);
        ArgumentNullException.ThrowIfNull(source.Prenom);
        ArgumentNullException.ThrowIfNull(source.Email);
        ArgumentNullException.ThrowIfNull(source.Actif);
        ArgumentNullException.ThrowIfNull(source.ProfilId);
        ArgumentNullException.ThrowIfNull(source.TypeUtilisateurCode);

        return new UtilisateurCommand
        {
            Nom = source.Nom,
            Prenom = source.Prenom,
            Email = source.Email,
            DateNaissance = source.DateNaissance,
            Adresse = source.Adresse,
            Actif = source.Actif.Value,
            ProfilId = source.ProfilId.Value,
            TypeUtilisateurCode = source.TypeUtilisateurCode.Value
        };
    }

    /// <summary>
    /// Mappe 'UtilisateurWrite' vers 'UtilisateurCommand'.
    /// </summary>
    /// <param name="source">Instance de 'UtilisateurWrite'.</param>
    /// <param name="dest">Instance pré-existante de 'UtilisateurCommand'.</param>
    /// <returns>L'instance pré-existante de 'UtilisateurCommand'.</returns>
    public static UtilisateurCommand ToUtilisateurCommand(this UtilisateurWrite source, UtilisateurCommand dest)
    {
        ArgumentNullException.ThrowIfNull(source.Nom);
        ArgumentNullException.ThrowIfNull(source.Prenom);
        ArgumentNullException.ThrowIfNull(source.Email);
        ArgumentNullException.ThrowIfNull(source.Actif);
        ArgumentNullException.ThrowIfNull(source.ProfilId);
        ArgumentNullException.ThrowIfNull(source.TypeUtilisateurCode);

        dest.Nom = source.Nom;
        dest.Prenom = source.Prenom;
        dest.Email = source.Email;
        dest.DateNaissance = source.DateNaissance;
        dest.Adresse = source.Adresse;
        dest.Actif = source.Actif.Value;
        dest.ProfilId = source.ProfilId.Value;
        dest.TypeUtilisateurCode = source.TypeUtilisateurCode.Value;
        return dest;
    }
}
