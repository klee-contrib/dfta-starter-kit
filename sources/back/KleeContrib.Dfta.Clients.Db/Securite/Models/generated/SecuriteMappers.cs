﻿////
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
    /// Crée une nouvelle instance de 'UtilisateurItemQuery'.
    /// </summary>
    /// <param name="utilisateur">Instance de 'Utilisateur'.</param>
    /// <returns>Une nouvelle instance de 'UtilisateurItemQuery'.</returns>
    public static UtilisateurItemQuery CreateUtilisateurItemQuery(Utilisateur utilisateur)
    {
        ArgumentNullException.ThrowIfNull(utilisateur);

        return new UtilisateurItemQuery
        {
            Id = utilisateur.Id,
            Nom = utilisateur.Nom,
            Prenom = utilisateur.Prenom,
            Email = utilisateur.Email,
            TypeUtilisateurCode = utilisateur.TypeUtilisateurCode
        };
    }

    /// <summary>
    /// Crée une nouvelle instance de 'UtilisateurQuery'.
    /// </summary>
    /// <param name="utilisateur">Instance de 'Utilisateur'.</param>
    /// <returns>Une nouvelle instance de 'UtilisateurQuery'.</returns>
    public static UtilisateurQuery CreateUtilisateurQuery(Utilisateur utilisateur)
    {
        ArgumentNullException.ThrowIfNull(utilisateur);

        return new UtilisateurQuery
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
