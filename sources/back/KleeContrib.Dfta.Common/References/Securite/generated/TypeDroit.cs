﻿////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;

namespace KleeContrib.Dfta.Common.References.Securite;

/// <summary>
/// Type de droit.
/// </summary>
[Reference(true)]
[DefaultProperty(nameof(Libelle))]
[Table("type_droits")]
public partial record TypeDroit
{
    /// <summary>
    /// Valeurs possibles de la liste de référence TypeDroit.
    /// </summary>
    public enum Codes
    {
        /// <summary>
        /// Administration.
        /// </summary>
        ADMIN,

        /// <summary>
        /// Lecture.
        /// </summary>
        READ,

        /// <summary>
        /// Ecriture.
        /// </summary>
        WRITE
    }

    /// <summary>
    /// Code du type de droit.
    /// </summary>
    [Column("tdr_code")]
    [Domain(Domains.Code)]
    [Key]
    public Codes? Code { get; set; }

    /// <summary>
    /// Libellé du type de droit.
    /// </summary>
    [Column("tdr_libelle")]
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string Libelle { get; set; }
}
