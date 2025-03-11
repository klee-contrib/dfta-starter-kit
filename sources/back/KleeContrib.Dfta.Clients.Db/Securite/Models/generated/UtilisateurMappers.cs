////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Queries.Models;

namespace KleeContrib.Dfta.Clients.Db.Securite.Models;

/// <summary>
/// Mappers pour le module 'Securite.Utilisateur'.
/// </summary>
public static class UtilisateurMappers
{
    /// <summary>
    /// Crée une nouvelle instance de 'UtilisateurItem'.
    /// </summary>
    /// <param name="utilisateur">Instance de 'Utilisateur'.</param>
    /// <returns>Une nouvelle instance de 'UtilisateurItem'.</returns>
    public static UtilisateurItem CreateUtilisateurItem(Utilisateur utilisateur)
    {
        ArgumentNullException.ThrowIfNull(utilisateur);

        return new UtilisateurItem
        {
            Id = utilisateur.Id,
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            Email = utilisateur.Email,
            TypeUtilisateurCode = utilisateur.TypeUtilisateurCode
        };
    }

    /// <summary>
    /// Crée une nouvelle instance de 'UtilisateurRead'.
    /// </summary>
    /// <param name="utilisateur">Instance de 'Utilisateur'.</param>
    /// <returns>Une nouvelle instance de 'UtilisateurRead'.</returns>
    public static UtilisateurRead CreateUtilisateurRead(Utilisateur utilisateur)
    {
        ArgumentNullException.ThrowIfNull(utilisateur);

        return new UtilisateurRead
        {
            Id = utilisateur.Id,
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            Email = utilisateur.Email,
            DateNaissance = utilisateur.DateNaissance,
            Adresse = utilisateur.Adresse,
            Actif = utilisateur.Actif,
            ProfilId = utilisateur.ProfilId,
            TypeUtilisateurCode = utilisateur.TypeUtilisateurCode,
            DateCreation = utilisateur.DateCreation,
            DateModification = utilisateur.DateModification
        };
    }

    /// <summary>
    /// Mappe 'UtilisateurCommand' vers 'Utilisateur'.
    /// </summary>
    /// <param name="source">Instance de 'UtilisateurCommand'.</param>
    /// <returns>Une nouvelle instance de 'Utilisateur'.</returns>
    public static Utilisateur ToUtilisateur(this UtilisateurCommand source)
    {
        return new Utilisateur
        {
            Nom = source.Nom,
            Prenom = source.Prenom,
            Email = source.Email,
            DateNaissance = source.DateNaissance,
            Adresse = source.Adresse,
            Actif = source.Actif,
            ProfilId = source.ProfilId,
            TypeUtilisateurCode = source.TypeUtilisateurCode
        };
    }

    /// <summary>
    /// Mappe 'UtilisateurCommand' vers 'Utilisateur'.
    /// </summary>
    /// <param name="source">Instance de 'UtilisateurCommand'.</param>
    /// <param name="dest">Instance pré-existante de 'Utilisateur'.</param>
    /// <returns>L'instance pré-existante de 'Utilisateur'.</returns>
    public static Utilisateur ToUtilisateur(this UtilisateurCommand source, Utilisateur dest)
    {
        dest.Nom = source.Nom;
        dest.Prenom = source.Prenom;
        dest.Email = source.Email;
        dest.DateNaissance = source.DateNaissance;
        dest.Adresse = source.Adresse;
        dest.Actif = source.Actif;
        dest.ProfilId = source.ProfilId;
        dest.TypeUtilisateurCode = source.TypeUtilisateurCode;
        return dest;
    }
}
