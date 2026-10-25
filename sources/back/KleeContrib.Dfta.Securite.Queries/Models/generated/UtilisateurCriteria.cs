////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using Kinetix.Search.Models;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Queries.Models;

/// <summary>
/// Critère de recherche pour les utilisateurs.
/// </summary>
public partial record UtilisateurCriteria : ICriteria
{
    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [StringLength(100)]
    public string? Nom { get; set; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [StringLength(100)]
    public string? Prenom { get; set; }

    /// <summary>
    /// Email de l'utilisateur.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Age de l'utilisateur.
    /// </summary>
    public DateOnly? DateNaissance { get; set; }

    /// <summary>
    /// Si l'utilisateur est actif.
    /// </summary>
    public bool? Actif { get; set; }

    /// <summary>
    /// Profil de l'utilisateur.
    /// </summary>
    public int? ProfilId { get; set; }

    /// <summary>
    /// Type d'utilisateur.
    /// </summary>
    [ReferencedType(typeof(TypeUtilisateur))]
    public TypeUtilisateur.Codes? TypeUtilisateurCode { get; set; }

    /// <summary>
    /// Critère de recherche.
    /// </summary>
    [StringLength(100)]
    public string? Query { get; set; }

    /// <summary>
    /// Liste des champs sur lesquels rechercher.
    /// </summary>
    public IList<string>? SearchFields { get; set; }

    /// <summary>
    /// Liste des champs à inclure dans la recherche (si non renseigné (ou vide) : tous les champs seront inclus).
    /// </summary>
    public IList<string>? SourceFields { get; set; }
}
