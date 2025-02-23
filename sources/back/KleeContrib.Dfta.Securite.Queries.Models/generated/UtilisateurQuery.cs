////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Queries.Models;

/// <summary>
/// Détail d'un utilisateur en lecture.
/// </summary>
public partial record UtilisateurQuery
{
    /// <summary>
    /// Id de l'utilisateur.
    /// </summary>
    [Domain(Domains.Id)]
    public required int Id { get; set; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Nom { get; set; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Prenom { get; set; }

    /// <summary>
    /// Email de l'utilisateur.
    /// </summary>
    [Domain(Domains.Email)]
    public required string Email { get; set; }

    /// <summary>
    /// Age de l'utilisateur.
    /// </summary>
    [Domain(Domains.Date)]
    public DateOnly? DateNaissance { get; set; }

    /// <summary>
    /// Adresse de l'utilisateur.
    /// </summary>
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string? Adresse { get; set; }

    /// <summary>
    /// Si l'utilisateur est actif.
    /// </summary>
    [Domain(Domains.Booleen)]
    public bool Actif { get; set; } = true;

    /// <summary>
    /// Profil de l'utilisateur.
    /// </summary>
    [Domain(Domains.Id)]
    public required int ProfilId { get; set; }

    /// <summary>
    /// Type d'utilisateur.
    /// </summary>
    [ReferencedType(typeof(TypeUtilisateur))]
    [Domain(Domains.Code)]
    public TypeUtilisateur.Codes TypeUtilisateurCode { get; set; } = TypeUtilisateur.Codes.GEST;

    /// <summary>
    /// Date de création de l'utilisateur.
    /// </summary>
    [Domain(Domains.DateHeure)]
    public required DateTime DateCreation { get; set; }

    /// <summary>
    /// Date de modification de l'utilisateur.
    /// </summary>
    [Domain(Domains.DateHeure)]
    public DateTime? DateModification { get; set; }
}
