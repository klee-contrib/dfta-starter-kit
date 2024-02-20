////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Queries.Models;

/// <summary>
/// Détail d'un utilisateur en liste.
/// </summary>
public partial record UtilisateurItem
{
    /// <summary>
    /// Id de l'utilisateur.
    /// </summary>
    [Required]
    [Domain(Domains.Id)]
    public int? Id { get; set; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string Nom { get; set; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string Prenom { get; set; }

    /// <summary>
    /// Email de l'utilisateur.
    /// </summary>
    [Required]
    [Domain(Domains.Email)]
    public string Email { get; set; }

    /// <summary>
    /// Type d'utilisateur.
    /// </summary>
    [Required]
    [ReferencedType(typeof(TypeUtilisateur))]
    [Domain(Domains.Code)]
    public TypeUtilisateur.Codes? TypeUtilisateurCode { get; set; } = TypeUtilisateur.Codes.GEST;
}
