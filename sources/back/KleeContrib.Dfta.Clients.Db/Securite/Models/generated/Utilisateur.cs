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
/// Utilisateur de l'application.
/// </summary>
[Table("utilisateurs")]
public partial record Utilisateur
{
    /// <summary>
    /// user1.
    /// </summary>
    public const string User1Email = "test1@test.com";

    /// <summary>
    /// user2.
    /// </summary>
    public const string User2Email = "test2@test.com";

    /// <summary>
    /// user3.
    /// </summary>
    public const string User3Email = "test3@test.com";

    /// <summary>
    /// user4.
    /// </summary>
    public const string User4Email = "test4@test.com";

    /// <summary>
    /// user5.
    /// </summary>
    public const string User5Email = "test5@test.com";

    /// <summary>
    /// user6.
    /// </summary>
    public const string User6Email = "test6@test.com";

    /// <summary>
    /// user7.
    /// </summary>
    public const string User7Email = "test7@test.com";

    /// <summary>
    /// user8.
    /// </summary>
    public const string User8Email = "test8@test.com";

    /// <summary>
    /// Id de l'utilisateur.
    /// </summary>
    [Column("uti_id")]
    [Domain(Domains.Id)]
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Column("uti_nom")]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Nom { get; set; }

    /// <summary>
    /// Nom de l'utilisateur.
    /// </summary>
    [Column("uti_prenom")]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public required string Prenom { get; set; }

    /// <summary>
    /// Email de l'utilisateur.
    /// </summary>
    [Column("uti_email")]
    [Domain(Domains.Email)]
    public required string Email { get; set; }

    /// <summary>
    /// Age de l'utilisateur.
    /// </summary>
    [Column("uti_date_naissance")]
    [Domain(Domains.Date)]
    public DateOnly? DateNaissance { get; set; }

    /// <summary>
    /// Adresse de l'utilisateur.
    /// </summary>
    [Column("uti_adresse")]
    [Domain(Domains.Libelle)]
    [StringLength(100)]
    public string? Adresse { get; set; }

    /// <summary>
    /// Si l'utilisateur est actif.
    /// </summary>
    [Column("uti_actif")]
    [Domain(Domains.Booleen)]
    public bool Actif { get; set; } = true;

    /// <summary>
    /// Profil de l'utilisateur.
    /// </summary>
    [Column("pro_id")]
    [Domain(Domains.Id)]
    public required int ProfilId { get; set; }

    /// <summary>
    /// Type d'utilisateur.
    /// </summary>
    [Column("tut_code")]
    [ReferencedType(typeof(TypeUtilisateur))]
    [Domain(Domains.Code)]
    public TypeUtilisateur.Codes TypeUtilisateurCode { get; set; } = TypeUtilisateur.Codes.GEST;

    /// <summary>
    /// Date de création de l'utilisateur.
    /// </summary>
    [Column("uti_date_creation")]
    [Domain(Domains.DateHeure)]
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date de modification de l'utilisateur.
    /// </summary>
    [Column("uti_date_modification")]
    [Domain(Domains.DateHeure)]
    public DateTime? DateModification { get; set; }
}
