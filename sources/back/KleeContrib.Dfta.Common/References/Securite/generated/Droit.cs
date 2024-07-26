////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kinetix.Modeling.Annotations;

namespace KleeContrib.Dfta.Common.References.Securite;

/// <summary>
/// Droits de l'application.
/// </summary>
[Reference(true)]
[DefaultProperty(nameof(Libelle))]
[Table("droits")]
public partial record Droit
{
    /// <summary>
    /// Valeurs possibles de la liste de référence Droit.
    /// </summary>
    public enum Codes
    {
        /// <summary>
        /// Création.
        /// </summary>
        CREATE,

        /// <summary>
        /// Suppression.
        /// </summary>
        DELETE,

        /// <summary>
        /// Lecture.
        /// </summary>
        READ,

        /// <summary>
        /// Mise à jour.
        /// </summary>
        UPDATE
    }

    /// <summary>
    /// Code du droit.
    /// </summary>
    [Column("dro_code")]
    [Domain(Domains.Code)]
    [Key]
    public required Codes Code { get; set; }

    /// <summary>
    /// Libellé du droit.
    /// </summary>
    [Column("dro_libelle")]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Libelle { get; set; }

    /// <summary>
    /// Type de profil pouvant faire l'action.
    /// </summary>
    [Column("tdr_code")]
    [ReferencedType(typeof(TypeDroit))]
    [Domain(Domains.Code)]
    public required TypeDroit.Codes TypeDroitCode { get; set; }
}
