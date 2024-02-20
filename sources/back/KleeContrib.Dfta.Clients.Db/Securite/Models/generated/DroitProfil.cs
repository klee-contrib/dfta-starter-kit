////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Clients.Db.Securite.Models;

/// <summary>
/// N-N droits - profils.
/// </summary>
[Table("droit_profils")]
public partial record DroitProfil
{
    /// <summary>
    /// Droit.
    /// </summary>
    [Column("dro_code")]
    [Required]
    [ReferencedType(typeof(Droit))]
    [Domain(Domains.Code)]
    public Droit.Codes? DroitCode { get; set; }

    /// <summary>
    /// Profil.
    /// </summary>
    [Column("pro_id")]
    [Required]
    [Domain(Domains.Id)]
    public int? ProfilId { get; set; }
}
