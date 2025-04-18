﻿////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Api.Models.Commands.Securite;

/// <summary>
/// Détail d'un profil en écriture.
/// </summary>
public partial record ProfilWrite
{
    /// <summary>
    /// Libellé du profil.
    /// </summary>
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string? Libelle { get; set; }

    /// <summary>
    /// Code du droit.
    /// </summary>
    [Required]
    [ReferencedType(typeof(Droit))]
    [Domain(Domains.CodeListe)]
    public Droit.Codes[]? DroitCodes { get; set; }
}
