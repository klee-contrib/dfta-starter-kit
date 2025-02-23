////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Annotations;
using KleeContrib.Dfta.Common;
using KleeContrib.Dfta.Common.References.Securite;

namespace KleeContrib.Dfta.Securite.Queries.Models;

/// <summary>
/// Détail d'un profil en lecture.
/// </summary>
public partial record ProfilQuery
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
    /// Date de création de l'utilisateur.
    /// </summary>
    [Domain(Domains.DateHeure)]
    public required DateTime DateCreation { get; set; }

    /// <summary>
    /// Date de modification de l'utilisateur.
    /// </summary>
    [Domain(Domains.DateHeure)]
    public DateTime? DateModification { get; set; }

    /// <summary>
    /// Code du droit.
    /// </summary>
    [ReferencedType(typeof(Droit))]
    [Domain(Domains.CodeListe)]
    public required Droit.Codes[] DroitCodes { get; set; }

    /// <summary>
    /// Utilisateurs ayant ce profil.
    /// </summary>
    public ICollection<UtilisateurItemQuery> Utilisateurs { get; set; } = new List<UtilisateurItemQuery>();
}
