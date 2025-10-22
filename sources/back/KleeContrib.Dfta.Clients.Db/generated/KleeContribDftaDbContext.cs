////
//// ATTENTION CE FICHIER EST GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Clients.Db.Common.Models;
using KleeContrib.Dfta.Clients.Db.Securite.Models;
using KleeContrib.Dfta.Common.References.Securite;
using Microsoft.EntityFrameworkCore;

namespace KleeContrib.Dfta.Clients.Db;

/// <summary>
/// DbContext généré pour Entity Framework Core.
/// </summary>
public partial class KleeContribDftaDbContext : DbContext
{
    /// <summary>
    /// Constructeur par défaut.
    /// </summary>
    /// <param name="options">Options du DbContext.</param>
    public KleeContribDftaDbContext(DbContextOptions<KleeContribDftaDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Accès à l'entité Droit.
    /// </summary>
    public DbSet<Droit> Droits { get; set; }

    /// <summary>
    /// Accès à l'entité DroitProfil.
    /// </summary>
    public DbSet<DroitProfil> DroitProfils { get; set; }

    /// <summary>
    /// Accès à l'entité Profil.
    /// </summary>
    public DbSet<Profil> Profils { get; set; }

    /// <summary>
    /// Accès à l'entité Traduction.
    /// </summary>
    public DbSet<Traduction> Traductions { get; set; }

    /// <summary>
    /// Accès à l'entité TypeDroit.
    /// </summary>
    public DbSet<TypeDroit> TypeDroits { get; set; }

    /// <summary>
    /// Accès à l'entité TypeUtilisateur.
    /// </summary>
    public DbSet<TypeUtilisateur> TypeUtilisateurs { get; set; }

    /// <summary>
    /// Accès à l'entité Utilisateur.
    /// </summary>
    public DbSet<Utilisateur> Utilisateurs { get; set; }

    /// <summary>
    /// Personalisation du modèle.
    /// </summary>
    /// <param name="modelBuilder">L'objet de construction du modèle.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Droit>().Property(p => p.Code).HasConversion<string>().HasMaxLength(10);
        modelBuilder.Entity<Droit>().Property(p => p.TypeDroitCode).HasConversion<string>().HasMaxLength(10);
        modelBuilder.Entity<DroitProfil>().Property(p => p.DroitCode).HasConversion<string>().HasMaxLength(10);
        modelBuilder.Entity<TypeDroit>().Property(p => p.Code).HasConversion<string>().HasMaxLength(10);
        modelBuilder.Entity<TypeUtilisateur>().Property(p => p.Code).HasConversion<string>().HasMaxLength(10);
        modelBuilder.Entity<Utilisateur>().Property(p => p.TypeUtilisateurCode).HasConversion<string>().HasMaxLength(10);

        modelBuilder.Entity<DroitProfil>().HasKey(p => new { p.DroitCode, p.ProfilId });
        modelBuilder.Entity<Traduction>().HasKey(p => new { p.ResourceKey, p.Locale });

        modelBuilder.Entity<Droit>().HasOne<TypeDroit>().WithMany().HasForeignKey(p => p.TypeDroitCode).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<DroitProfil>().HasOne<Droit>().WithMany().HasForeignKey(p => p.DroitCode).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<DroitProfil>().HasOne<Profil>().WithMany().HasForeignKey(p => p.ProfilId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Utilisateur>().HasOne<Profil>().WithMany().HasForeignKey(p => p.ProfilId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Utilisateur>().HasOne<TypeUtilisateur>().WithMany().HasForeignKey(p => p.TypeUtilisateurCode).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Utilisateur>().HasIndex(p => p.Email).IsUnique();

        modelBuilder.Entity<Traduction>().HasIndex(p => p.ResourceKey);
        modelBuilder.Entity<Droit>().HasIndex(p => p.Libelle);
        modelBuilder.Entity<TypeDroit>().HasIndex(p => p.Libelle);
        modelBuilder.Entity<TypeUtilisateur>().HasIndex(p => p.Libelle);

        modelBuilder.Entity<Droit>().HasData(
            new Droit { Code = Droit.Codes.CREATE, Libelle = "securite.droit.values.Create", TypeDroitCode = TypeDroit.Codes.WRITE },
            new Droit { Code = Droit.Codes.READ, Libelle = "securite.droit.values.Read", TypeDroitCode = TypeDroit.Codes.READ },
            new Droit { Code = Droit.Codes.UPDATE, Libelle = "securite.droit.values.Update", TypeDroitCode = TypeDroit.Codes.WRITE },
            new Droit { Code = Droit.Codes.DELETE, Libelle = "securite.droit.values.Delete", TypeDroitCode = TypeDroit.Codes.ADMIN });
        modelBuilder.Entity<DroitProfil>().HasData(
            new DroitProfil { DroitCode = Droit.Codes.CREATE, ProfilId = 1 },
            new DroitProfil { DroitCode = Droit.Codes.READ, ProfilId = 2 },
            new DroitProfil { DroitCode = Droit.Codes.UPDATE, ProfilId = 3 },
            new DroitProfil { DroitCode = Droit.Codes.DELETE, ProfilId = 4 });
        modelBuilder.Entity<Profil>().HasData(
            new Profil { Id = 1, Libelle = "Profil 1", DateCreation = DateTime.UtcNow },
            new Profil { Id = 2, Libelle = "Profil 2", DateCreation = DateTime.UtcNow },
            new Profil { Id = 3, Libelle = "Profil 3", DateCreation = DateTime.UtcNow },
            new Profil { Id = 4, Libelle = "Profil 4", DateCreation = DateTime.UtcNow });
        modelBuilder.Entity<TypeDroit>().HasData(
            new TypeDroit { Code = TypeDroit.Codes.READ, Libelle = "securite.typeDroit.values.Read" },
            new TypeDroit { Code = TypeDroit.Codes.WRITE, Libelle = "securite.typeDroit.values.Write" },
            new TypeDroit { Code = TypeDroit.Codes.ADMIN, Libelle = "securite.typeDroit.values.Admin" });
        modelBuilder.Entity<TypeUtilisateur>().HasData(
            new TypeUtilisateur { Code = TypeUtilisateur.Codes.ADMIN, Libelle = "securite.typeUtilisateur.values.Admin" },
            new TypeUtilisateur { Code = TypeUtilisateur.Codes.GEST, Libelle = "securite.typeUtilisateur.values.Gestionnaire" },
            new TypeUtilisateur { Code = TypeUtilisateur.Codes.CLIENT, Libelle = "securite.typeUtilisateur.values.Client" });
        modelBuilder.Entity<Utilisateur>().HasData(
            new Utilisateur { Id = 1, Nom = "Jean", Prenom = "Michel", Actif = true, ProfilId = 1, TypeUtilisateurCode = TypeUtilisateur.Codes.ADMIN, Email = "test1@test.com", DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = 2, Nom = "Gerard", Prenom = "Jugnos", Actif = true, ProfilId = 2, TypeUtilisateurCode = TypeUtilisateur.Codes.GEST, Email = "test2@test.com", DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = 3, Nom = "Gad", Prenom = "El", Actif = true, ProfilId = 3, TypeUtilisateurCode = TypeUtilisateur.Codes.CLIENT, Email = "test3@test.com", DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = 4, Nom = "Bernard", Prenom = "Bruno", Actif = true, ProfilId = 4, TypeUtilisateurCode = TypeUtilisateur.Codes.ADMIN, Email = "test4@test.com", DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = 5, Nom = "Sisi", Prenom = "Brindacier", Actif = true, ProfilId = 1, TypeUtilisateurCode = TypeUtilisateur.Codes.GEST, Email = "test5@test.com", DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = 6, Nom = "Bibi", Prenom = "Baba", Actif = true, ProfilId = 2, TypeUtilisateurCode = TypeUtilisateur.Codes.CLIENT, Email = "test6@test.com", DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = 7, Nom = "Dédé", Prenom = "Dédé", Actif = true, ProfilId = 3, TypeUtilisateurCode = TypeUtilisateur.Codes.GEST, Email = "test7@test.com", DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = 8, Nom = "Ran", Prenom = "Tanplan", Actif = true, ProfilId = 4, TypeUtilisateurCode = TypeUtilisateur.Codes.ADMIN, Email = "test8@test.com", DateCreation = DateTime.UtcNow });

        AddFrFRResources(modelBuilder);
        AddEnUSResources(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void AddFrFRResources(ModelBuilder modelBuilder);

    partial void AddEnUSResources(ModelBuilder modelBuilder);

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
