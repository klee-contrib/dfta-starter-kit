////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;

namespace KleeContrib.Dfta.Clients.Db.Securite.Models;

/// <summary>
/// Profil des utilisateurs.
/// </summary>
[Table("profils")]
public partial record Profil
{
    /// <summary>
    /// Id technique.
    /// </summary>
    [Column("pro_id")]
    [Domain(Domains.Id)]
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Libellé du profil.
    /// </summary>
    [Column("pro_libelle")]
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Libelle { get; set; }

    /// <summary>
    /// Date de création de l'utilisateur.
    /// </summary>
    [Column("pro_date_creation")]
    [Required]
    [Domain(Domains.DateHeure)]
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date de modification de l'utilisateur.
    /// </summary>
    [Column("pro_date_modification")]
    [Domain(Domains.DateHeure)]
    public DateTime? DateModification { get; set; }
}
