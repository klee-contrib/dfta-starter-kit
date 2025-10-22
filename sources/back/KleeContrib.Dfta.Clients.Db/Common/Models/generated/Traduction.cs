////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;

namespace KleeContrib.Dfta.Clients.Db.Common.Models;

/// <summary>
/// Classe pour contenir les traductions en base de données.
/// </summary>
[Table("traductions")]
public partial record Traduction
{
    /// <summary>
    /// Clé de traduction.
    /// </summary>
    [Column("trd_resource_key")]
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string ResourceKey { get; set; }

    /// <summary>
    /// Locale.
    /// </summary>
    [Column("trd_locale")]
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Locale { get; set; }

    /// <summary>
    /// Valeur.
    /// </summary>
    [Column("trd_label")]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Label { get; set; }
}
