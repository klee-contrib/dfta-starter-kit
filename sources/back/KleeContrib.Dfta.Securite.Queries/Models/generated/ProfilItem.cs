////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;

namespace KleeContrib.Dfta.Securite.Queries.Models;

/// <summary>
/// Détail d'un profil en liste.
/// </summary>
public partial record ProfilItem
{
    /// <summary>
    /// Id technique.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Libellé du profil.
    /// </summary>
    [StringLength(100)]
    public required string Libelle { get; init; }

    /// <summary>
    /// Nombre d'utilisateurs affectés au profil.
    /// </summary>
    public required long NombreUtilisateurs { get; init; }
}
