////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;

namespace KleeContrib.Dfta.Api.Models.Queries;

/// <summary>
/// Détail d'un profil en liste.
/// </summary>
public partial record ProfilItem
{
    /// <summary>
    /// Id technique.
    /// </summary>
    [Domain(Domains.Id)]
    public required int Id { get; set; }

    /// <summary>
    /// Libellé du profil.
    /// </summary>
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Libelle { get; set; }

    /// <summary>
    /// Nombre d'utilisateurs affectés au profil.
    /// </summary>
    [Domain(Domains.Entier)]
    public required long NombreUtilisateurs { get; set; }
}
