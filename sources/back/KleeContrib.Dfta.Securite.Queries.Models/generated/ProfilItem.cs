////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;

namespace KleeContrib.Dfta.Securite.Queries.Models;

/// <summary>
/// Détail d'un profil en liste.
/// </summary>
public partial record ProfilItem
{
    /// <summary>
    /// Id technique.
    /// </summary>
    [Required]
    [Domain(Domains.Id)]
    public int? Id { get; set; }

    /// <summary>
    /// Libellé du profil.
    /// </summary>
    [Required]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string Libelle { get; set; }

    /// <summary>
    /// Nombre d'utilisateurs affectés au profil.
    /// </summary>
    [Required]
    [Domain(Domains.Entier)]
    public long? NombreUtilisateurs { get; set; }
}
