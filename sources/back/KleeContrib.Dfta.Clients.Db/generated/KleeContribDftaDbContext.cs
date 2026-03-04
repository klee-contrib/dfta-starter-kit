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
public partial class KleeContribDftaDbContext(DbContextOptions<KleeContribDftaDbContext> options) : DbContext(options)
{
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

        modelBuilder.Entity<Droit>().HasOne<TypeDroit>().WithMany().HasForeignKey(p => p.TypeDroitCode).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<DroitProfil>().HasOne<Droit>().WithMany().HasForeignKey(p => p.DroitCode).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<DroitProfil>().HasOne<Profil>().WithMany().HasForeignKey(p => p.ProfilId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Utilisateur>().HasOne<Profil>().WithMany().HasForeignKey(p => p.ProfilId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Utilisateur>().HasOne<TypeUtilisateur>().WithMany().HasForeignKey(p => p.TypeUtilisateurCode).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DroitProfil>().HasKey(p => new { p.DroitCode, p.ProfilId });
        modelBuilder.Entity<Traduction>().HasKey(p => new { p.ResourceKey, p.Locale });

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
            new DroitProfil { DroitCode = Droit.Codes.CREATE, ProfilId = Profil.Profil1Id },
            new DroitProfil { DroitCode = Droit.Codes.READ, ProfilId = Profil.Profil2Id },
            new DroitProfil { DroitCode = Droit.Codes.UPDATE, ProfilId = Profil.Profil3Id },
            new DroitProfil { DroitCode = Droit.Codes.DELETE, ProfilId = Profil.Profil4Id });
        modelBuilder.Entity<Profil>().HasData(
            new Profil { Id = Profil.Profil1Id, Libelle = "Profil 1", DateCreation = DateTime.UtcNow },
            new Profil { Id = Profil.Profil2Id, Libelle = "Profil 2", DateCreation = DateTime.UtcNow },
            new Profil { Id = Profil.Profil3Id, Libelle = "Profil 3", DateCreation = DateTime.UtcNow },
            new Profil { Id = Profil.Profil4Id, Libelle = "Profil 4", DateCreation = DateTime.UtcNow });
        modelBuilder.Entity<TypeDroit>().HasData(
            new TypeDroit { Code = TypeDroit.Codes.READ, Libelle = "securite.typeDroit.values.Read" },
            new TypeDroit { Code = TypeDroit.Codes.WRITE, Libelle = "securite.typeDroit.values.Write" },
            new TypeDroit { Code = TypeDroit.Codes.ADMIN, Libelle = "securite.typeDroit.values.Admin" });
        modelBuilder.Entity<TypeUtilisateur>().HasData(
            new TypeUtilisateur { Code = TypeUtilisateur.Codes.ADMIN, Libelle = "securite.typeUtilisateur.values.Admin" },
            new TypeUtilisateur { Code = TypeUtilisateur.Codes.GEST, Libelle = "securite.typeUtilisateur.values.Gestionnaire" },
            new TypeUtilisateur { Code = TypeUtilisateur.Codes.CLIENT, Libelle = "securite.typeUtilisateur.values.Client" });
        modelBuilder.Entity<Utilisateur>().HasData(
            new Utilisateur { Id = Utilisateur.User1Id, Nom = "Jean", Prenom = "Michel", Actif = true, ProfilId = Profil.Profil1Id, TypeUtilisateurCode = TypeUtilisateur.Codes.ADMIN, Email = Utilisateur.User1Email, DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = Utilisateur.User2Id, Nom = "Gerard", Prenom = "Jugnos", Actif = true, ProfilId = Profil.Profil2Id, TypeUtilisateurCode = TypeUtilisateur.Codes.GEST, Email = Utilisateur.User2Email, DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = Utilisateur.User3Id, Nom = "Gad", Prenom = "El", Actif = true, ProfilId = Profil.Profil3Id, TypeUtilisateurCode = TypeUtilisateur.Codes.CLIENT, Email = Utilisateur.User3Email, DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = Utilisateur.User4Id, Nom = "Bernard", Prenom = "Bruno", Actif = true, ProfilId = Profil.Profil4Id, TypeUtilisateurCode = TypeUtilisateur.Codes.ADMIN, Email = Utilisateur.User4Email, DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = Utilisateur.User5Id, Nom = "Sisi", Prenom = "Brindacier", Actif = true, ProfilId = Profil.Profil1Id, TypeUtilisateurCode = TypeUtilisateur.Codes.GEST, Email = Utilisateur.User5Email, DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = Utilisateur.User6Id, Nom = "Bibi", Prenom = "Baba", Actif = true, ProfilId = Profil.Profil2Id, TypeUtilisateurCode = TypeUtilisateur.Codes.CLIENT, Email = Utilisateur.User6Email, DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = Utilisateur.User7Id, Nom = "Dédé", Prenom = "Dédé", Actif = true, ProfilId = Profil.Profil3Id, TypeUtilisateurCode = TypeUtilisateur.Codes.GEST, Email = Utilisateur.User7Email, DateCreation = DateTime.UtcNow },
            new Utilisateur { Id = Utilisateur.User8Id, Nom = "Ran", Prenom = "Tanplan", Actif = true, ProfilId = Profil.Profil4Id, TypeUtilisateurCode = TypeUtilisateur.Codes.ADMIN, Email = Utilisateur.User8Email, DateCreation = DateTime.UtcNow });

        AddFrFRResources(modelBuilder);
        AddEnUSResources(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void AddEnUSResources(ModelBuilder modelBuilder);

    partial void AddFrFRResources(ModelBuilder modelBuilder);

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
