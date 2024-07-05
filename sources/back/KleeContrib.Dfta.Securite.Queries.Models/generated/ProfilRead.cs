////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Queries.Models;

/// <summary>
/// Détail d'un profil en lecture.
/// </summary>
public partial record ProfilRead
{
    /// <summary>
    /// Id technique.
    /// </summary>
    [Required]
    [Domain(Domains.Id)]
    public required int Id { get; set; }

    /// <summary>
    /// Libellé du profil.
    /// </summary>
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Libelle { get; set; }

    /// <summary>
    /// Date de création de l'utilisateur.
    /// </summary>
    [Required]
    [Domain(Domains.DateHeure)]
    public required DateTime DateCreation { get; set; }

    /// <summary>
    /// Date de modification de l'utilisateur.
    /// </summary>
    [Domain(Domains.DateHeure)]
    public DateTime? DateModification { get; set; }

    /// <summary>
    /// Code du droit.
    /// </summary>
    [Required]
    [ReferencedType(typeof(Droit))]
    [Domain(Domains.CodeListe)]
    public required Droit.Codes[] DroitCodes { get; set; }

    /// <summary>
    /// Utilisateurs ayant ce profil.
    /// </summary>
    [NotMapped]
    public ICollection<UtilisateurItem> Utilisateurs { get; set; } = new List<UtilisateurItem>();
}
