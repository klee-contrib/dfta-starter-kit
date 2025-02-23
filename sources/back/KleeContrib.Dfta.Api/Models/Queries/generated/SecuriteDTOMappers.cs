////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Securite.Queries.Models;

namespace KleeContrib.Dfta.Api.Models.Queries;

/// <summary>
/// Mappers pour le module 'Securite'.
/// </summary>
public static class SecuriteDTOMappers
{
    /// <summary>
    /// Crée une nouvelle instance de 'ProfilItem'.
    /// </summary>
    /// <param name="profilItemQuery">Instance de 'ProfilItemQuery'.</param>
    /// <returns>Une nouvelle instance de 'ProfilItem'.</returns>
    public static ProfilItem CreateProfilItem(ProfilItemQuery profilItemQuery)
    {
        ArgumentNullException.ThrowIfNull(profilItemQuery);

        return new ProfilItem
        {
            Id = profilItemQuery.Id,
            Libelle = profilItemQuery.Libelle,
            NombreUtilisateurs = profilItemQuery.NombreUtilisateurs
        };
    }

    /// <summary>
    /// Crée une nouvelle instance de 'ProfilRead'.
    /// </summary>
    /// <param name="profilQuery">Instance de 'ProfilQuery'.</param>
    /// <param name="utilisateurs">Utilisateurs ayant ce profil.</param>
    /// <returns>Une nouvelle instance de 'ProfilRead'.</returns>
    public static ProfilRead CreateProfilRead(ProfilQuery profilQuery, ICollection<UtilisateurItem> utilisateurs)
    {
        ArgumentNullException.ThrowIfNull(profilQuery);
        ArgumentNullException.ThrowIfNull(utilisateurs);

        return new ProfilRead
        {
            Id = profilQuery.Id,
            Libelle = profilQuery.Libelle,
            DateCreation = profilQuery.DateCreation,
            DateModification = profilQuery.DateModification,
            DroitCodes = profilQuery.DroitCodes,
            Utilisateurs = utilisateurs
        };
    }

    /// <summary>
    /// Crée une nouvelle instance de 'UtilisateurItem'.
    /// </summary>
    /// <param name="utilisateurItemQuery">Instance de 'UtilisateurItemQuery'.</param>
    /// <returns>Une nouvelle instance de 'UtilisateurItem'.</returns>
    public static UtilisateurItem CreateUtilisateurItem(UtilisateurItemQuery utilisateurItemQuery)
    {
        ArgumentNullException.ThrowIfNull(utilisateurItemQuery);

        return new UtilisateurItem
        {
            Id = utilisateurItemQuery.Id,
            Nom = utilisateurItemQuery.Nom,
            Prenom = utilisateurItemQuery.Prenom,
            Email = utilisateurItemQuery.Email,
            TypeUtilisateurCode = utilisateurItemQuery.TypeUtilisateurCode
        };
    }

    /// <summary>
    /// Crée une nouvelle instance de 'UtilisateurRead'.
    /// </summary>
    /// <param name="utilisateurQuery">Instance de 'UtilisateurQuery'.</param>
    /// <returns>Une nouvelle instance de 'UtilisateurRead'.</returns>
    public static UtilisateurRead CreateUtilisateurRead(UtilisateurQuery utilisateurQuery)
    {
        ArgumentNullException.ThrowIfNull(utilisateurQuery);

        return new UtilisateurRead
        {
            Id = utilisateurQuery.Id,
            Nom = utilisateurQuery.Nom,
            Prenom = utilisateurQuery.Prenom,
            Email = utilisateurQuery.Email,
            DateNaissance = utilisateurQuery.DateNaissance,
            Adresse = utilisateurQuery.Adresse,
            Actif = utilisateurQuery.Actif,
            ProfilId = utilisateurQuery.ProfilId,
            TypeUtilisateurCode = utilisateurQuery.TypeUtilisateurCode,
            DateCreation = utilisateurQuery.DateCreation,
            DateModification = utilisateurQuery.DateModification
        };
    }
}
