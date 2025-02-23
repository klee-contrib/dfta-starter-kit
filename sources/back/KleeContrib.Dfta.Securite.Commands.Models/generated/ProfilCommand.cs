////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Commands.Models;

/// <summary>
/// Détail d'un profil en écriture.
/// </summary>
public partial record ProfilCommand
{
    /// <summary>
    /// Libellé du profil.
    /// </summary>
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Libelle { get; set; }

    /// <summary>
    /// Code du droit.
    /// </summary>
    [ReferencedType(typeof(Droit))]
    [Domain(Domains.CodeListe)]
    public required Droit.Codes[] DroitCodes { get; set; }
}
