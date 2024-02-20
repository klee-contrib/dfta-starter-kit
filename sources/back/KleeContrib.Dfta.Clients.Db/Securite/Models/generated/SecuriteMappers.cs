////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Queries.Models;

namespace KleeContrib.Dfta.Clients.Db.Securite.Models;

/// <summary>
/// Mappers pour le module 'Securite'.
/// </summary>
public static class SecuriteMappers
{
    /// <summary>
    /// Crée une nouvelle instance de 'ProfilItem'.
    /// </summary>
    /// <param name="profil">Instance de 'Profil'.</param>
    /// <returns>Une nouvelle instance de 'ProfilItem'.</returns>
    public static ProfilItem CreateProfilItem(Profil profil)
    {
        ArgumentNullException.ThrowIfNull(profil);

        return new ProfilItem
        {
            Id = profil.Id,
            Libelle = profil.Libelle
        };
    }

    /// <summary>
    /// Crée une nouvelle instance de 'ProfilRead'.
    /// </summary>
    /// <param name="profil">Instance de 'Profil'.</param>
    /// <returns>Une nouvelle instance de 'ProfilRead'.</returns>
    public static ProfilRead CreateProfilRead(Profil profil)
    {
        ArgumentNullException.ThrowIfNull(profil);

        return new ProfilRead
        {
            Id = profil.Id,
            Libelle = profil.Libelle,
            DateCreation = profil.DateCreation,
            DateModification = profil.DateModification
        };
    }

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
    /// Mappe 'Profil' vers 'Profil'.
    /// </summary>
    /// <param name="source">Instance de 'Profil'.</param>
    /// <param name="dest">Instance pré-existante de 'Profil'. Une nouvelle instance sera créée si non spécifié.</param>
    /// <returns>Une instance de 'Profil'.</returns>
    public static Profil ToProfil(this Profil source, Profil dest = null)
    {
        dest ??= new Profil();
        dest.Libelle = source.Libelle;
        return dest;
    }

    /// <summary>
    /// Mappe 'ProfilItem' vers 'Profil'.
    /// </summary>
    /// <param name="source">Instance de 'ProfilItem'.</param>
    /// <param name="dest">Instance pré-existante de 'Profil'. Une nouvelle instance sera créée si non spécifié.</param>
    /// <returns>Une instance de 'Profil'.</returns>
    public static Profil ToProfil(this ProfilItem source, Profil dest = null)
    {
        dest ??= new Profil();
        dest.Id = source.Id;
        dest.Libelle = source.Libelle;
        return dest;
    }

    /// <summary>
    /// Mappe 'ProfilRead' vers 'Profil'.
    /// </summary>
    /// <param name="source">Instance de 'ProfilRead'.</param>
    /// <param name="dest">Instance pré-existante de 'Profil'. Une nouvelle instance sera créée si non spécifié.</param>
    /// <returns>Une instance de 'Profil'.</returns>
    public static Profil ToProfil(this ProfilRead source, Profil dest = null)
    {
        dest ??= new Profil();
        dest.Id = source.Id;
        dest.Libelle = source.Libelle;
        dest.DateCreation = source.DateCreation;
        dest.DateModification = source.DateModification;
        return dest;
    }

    /// <summary>
    /// Mappe 'ProfilWrite' vers 'Profil'.
    /// </summary>
    /// <param name="source">Instance de 'ProfilWrite'.</param>
    /// <param name="dest">Instance pré-existante de 'Profil'. Une nouvelle instance sera créée si non spécifié.</param>
    /// <returns>Une instance de 'Profil'.</returns>
    public static Profil ToProfil(this ProfilWrite source, Profil dest = null)
    {
        dest ??= new Profil();
        dest.Libelle = source.Libelle;
        return dest;
    }

    /// <summary>
    /// Mappe 'UtilisateurRead' vers 'Utilisateur'.
    /// </summary>
    /// <param name="source">Instance de 'UtilisateurRead'.</param>
    /// <param name="dest">Instance pré-existante de 'Utilisateur'. Une nouvelle instance sera créée si non spécifié.</param>
    /// <returns>Une instance de 'Utilisateur'.</returns>
    public static Utilisateur ToUtilisateur(this UtilisateurRead source, Utilisateur dest = null)
    {
        dest ??= new Utilisateur();
        dest.Id = source.Id;
        dest.Nom = source.Nom;
        dest.Prenom = source.Prenom;
        dest.Email = source.Email;
        dest.DateNaissance = source.DateNaissance;
        dest.Adresse = source.Adresse;
        dest.Actif = source.Actif;
        dest.ProfilId = source.ProfilId;
        dest.TypeUtilisateurCode = source.TypeUtilisateurCode;
        dest.DateCreation = source.DateCreation;
        dest.DateModification = source.DateModification;
        return dest;
    }

    /// <summary>
    /// Mappe 'UtilisateurWrite' vers 'Utilisateur'.
    /// </summary>
    /// <param name="source">Instance de 'UtilisateurWrite'.</param>
    /// <param name="dest">Instance pré-existante de 'Utilisateur'. Une nouvelle instance sera créée si non spécifié.</param>
    /// <returns>Une instance de 'Utilisateur'.</returns>
    public static Utilisateur ToUtilisateur(this UtilisateurWrite source, Utilisateur dest = null)
    {
        dest ??= new Utilisateur();
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
