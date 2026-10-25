////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    [StringLength(100)]
    public required string ResourceKey { get; set; }

    /// <summary>
    /// Locale.
    /// </summary>
    [Column("trd_locale")]
    [StringLength(100)]
    public required string Locale { get; set; }

    /// <summary>
    /// Valeur.
    /// </summary>
    [Column("trd_label")]
    [StringLength(100)]
    public required string Label { get; set; }
}
