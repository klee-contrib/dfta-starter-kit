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
    /// Mappe 'ProfilWrite' vers 'Profil'.
    /// </summary>
    /// <param name="source">Instance de 'ProfilWrite'.</param>
    /// <returns>Une nouvelle instance de 'Profil'.</returns>
    public static Profil ToProfil(this ProfilWrite source)
    {
        ArgumentNullException.ThrowIfNull(source.Libelle);

        return new Profil
        {
            Libelle = source.Libelle
        };
    }

    /// <summary>
    /// Mappe 'ProfilWrite' vers 'Profil'.
    /// </summary>
    /// <param name="source">Instance de 'ProfilWrite'.</param>
    /// <param name="dest">Instance pré-existante de 'Profil'.</param>
    /// <returns>L'instance pré-existante de 'Profil'.</returns>
    public static Profil ToProfil(this ProfilWrite source, Profil dest)
    {
        ArgumentNullException.ThrowIfNull(source.Libelle);

        dest.Libelle = source.Libelle;
        return dest;
    }

    /// <summary>
    /// Mappe 'UtilisateurWrite' vers 'Utilisateur'.
    /// </summary>
    /// <param name="source">Instance de 'UtilisateurWrite'.</param>
    /// <returns>Une nouvelle instance de 'Utilisateur'.</returns>
    public static Utilisateur ToUtilisateur(this UtilisateurWrite source)
    {
        ArgumentNullException.ThrowIfNull(source.Nom);
        ArgumentNullException.ThrowIfNull(source.Prenom);
        ArgumentNullException.ThrowIfNull(source.Email);
        ArgumentNullException.ThrowIfNull(source.Actif);
        ArgumentNullException.ThrowIfNull(source.ProfilId);
        ArgumentNullException.ThrowIfNull(source.TypeUtilisateurCode);

        return new Utilisateur
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
    /// Mappe 'UtilisateurWrite' vers 'Utilisateur'.
    /// </summary>
    /// <param name="source">Instance de 'UtilisateurWrite'.</param>
    /// <param name="dest">Instance pré-existante de 'Utilisateur'.</param>
    /// <returns>L'instance pré-existante de 'Utilisateur'.</returns>
    public static Utilisateur ToUtilisateur(this UtilisateurWrite source, Utilisateur dest)
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
