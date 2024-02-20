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
/// Type d'utilisateur.
/// </summary>
[Reference(true)]
[DefaultProperty(nameof(Libelle))]
[Table("type_utilisateurs")]
public partial record TypeUtilisateur
{
    /// <summary>
    /// Valeurs possibles de la liste de référence TypeUtilisateur.
    /// </summary>
    public enum Codes
    {
        /// <summary>
        /// Administrateur.
        /// </summary>
        ADMIN,

        /// <summary>
        /// Client.
        /// </summary>
        CLIENT,

        /// <summary>
        /// Gestionnaire.
        /// </summary>
        GEST
    }

    /// <summary>
    /// Code du type d'utilisateur.
    /// </summary>
    [Column("tut_code")]
    [Domain(Domains.Code)]
    [Key]
    public Codes? Code { get; set; }

    /// <summary>
    /// Libellé du type d'utilisateur.
    /// </summary>
    [Column("tut_libelle")]
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string Libelle { get; set; }
}
