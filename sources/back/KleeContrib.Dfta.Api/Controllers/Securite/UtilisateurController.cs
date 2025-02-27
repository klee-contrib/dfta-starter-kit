////
//// ATTENTION, CE FICHIER EST PARTIELLEMENT GENERE AUTOMATIQUEMENT !
////

using System.ComponentModel.DataAnnotations;
using Kinetix.Modeling.Exceptions;
using KleeContrib.Dfta.Api.Models.Commands;
using KleeContrib.Dfta.Common.References.Securite;
using KleeContrib.Dfta.Securite.Commands;
using KleeContrib.Dfta.Securite.Queries;
using KleeContrib.Dfta.Securite.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace KleeContrib.Dfta.Api.Securite;

/// <summary>
/// Contrôleur pour Utilisateur.
/// </summary>
public class UtilisateurController(IUtilisateurCommands commands, IUtilisateurDbQueries dbQueries, IUtilisateurMixedQueries mixedQueries) : Controller
{
    /// <summary>
    /// Ajoute un utilisateur
    /// </summary>
    /// <param name="utilisateur">Utilisateur à sauvegarder</param>
    /// <returns>Utilisateur sauvegardé</returns>
    [HttpPost("api/utilisateurs")]
    public async Task<UtilisateurRead> AddUtilisateur([FromBody] UtilisateurWrite utilisateur)
    {
        var id = await commands.AddUtilisateur(utilisateur.ToUtilisateurCommand());
        return await dbQueries.GetUtilisateur(id);
    }

    /// <summary>
    /// Ajoute une nouvelle photo pour un utilisateur.
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <param name="photo">Photo.</param>
    /// <returns>Task.</returns>
    [HttpPut("api/utilisateurs/{utiId:int}/photo")]
    public async Task AddUtilisateurPhoto(int utiId, [Required] IFormFile? photo = null)
    {
        if (photo!.ContentType != "image/jpeg")
        {
            throw new BusinessException("Seules les images au format *.jpg sont autorisées.");
        }

        using var file = photo!.OpenReadStream();
        await commands.AddUtilisateurPhoto(utiId, photo.FileName, file);
    }

    /// <summary>
    /// Supprime un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <returns>Task.</returns>
    [HttpDelete("api/utilisateurs/{utiId:int}")]
    public async Task DeleteUtilisateur(int utiId)
    {
        await commands.DeleteUtilisateur(utiId);
    }

    /// <summary>
    /// Supprime la photo d'un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <returns>Task.</returns>
    [HttpDelete("api/utilisateurs/{utiId:int}/photo")]
    public async Task DeleteUtilisateurPhoto(int utiId)
    {
        await commands.DeleteUtilisateurPhoto(utiId);
    }

    /// <summary>
    /// Charge le détail d'un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <returns>Le détail de l'utilisateur</returns>
    [HttpGet("api/utilisateurs/{utiId:int}")]
    public async Task<UtilisateurRead> GetUtilisateur(int utiId)
    {
        return await dbQueries.GetUtilisateur(utiId);
    }

    /// <summary>
    /// Charge la photo d'un utilisateur (si elle existe).
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <returns>Photo.</returns>
    [HttpGet("api/utilisateurs/{utiId:int}/photo")]
    public async Task<FileContentResult> GetUtilisateurPhoto(int utiId)
    {
        var file = await mixedQueries.GetUtilisateurPhoto(utiId);
        if (file == null)
        {
            Response.StatusCode = 204;
            return new FileContentResult([], "image/jpeg");
        }
        else
        {
            return new FileContentResult(file, "image/jpeg");
        }
    }

    /// <summary>
    /// Recherche des utilisateurs
    /// </summary>
    /// <param name="nom">Nom de l'utilisateur</param>
    /// <param name="prenom">Nom de l'utilisateur</param>
    /// <param name="email">Email de l'utilisateur</param>
    /// <param name="dateNaissance">Age de l'utilisateur</param>
    /// <param name="actif">Si l'utilisateur est actif</param>
    /// <param name="profilId">Profil de l'utilisateur</param>
    /// <param name="typeUtilisateurCode">Type d'utilisateur</param>
    /// <returns>Utilisateurs matchant les critères</returns>
    [HttpGet("api/utilisateurs")]
    public async Task<ICollection<UtilisateurItem>> SearchUtilisateur(string? nom = null, string? prenom = null, string? email = null, DateOnly? dateNaissance = null, bool? actif = null, int? profilId = null, TypeUtilisateur.Codes? typeUtilisateurCode = null)
    {
        return await dbQueries.SearchUtilisateur(nom, prenom, email, dateNaissance, actif, profilId, typeUtilisateurCode);
    }

    /// <summary>
    /// Sauvegarde un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <param name="utilisateur">Utilisateur à sauvegarder</param>
    /// <returns>Utilisateur sauvegardé</returns>
    [HttpPut("api/utilisateurs/{utiId:int}")]
    public async Task<UtilisateurRead> UpdateUtilisateur(int utiId, [FromBody] UtilisateurWrite utilisateur)
    {
        await commands.UpdateUtilisateur(utiId, utilisateur.ToUtilisateurCommand());
        return await dbQueries.GetUtilisateur(utiId);
    }
}