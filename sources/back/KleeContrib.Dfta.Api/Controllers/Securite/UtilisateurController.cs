﻿////
//// ATTENTION, CE FICHIER EST PARTIELLEMENT GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Common.References.Securite;
using KleeContrib.Dfta.Securite.Commands;
using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Queries;
using KleeContrib.Dfta.Securite.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace KleeContrib.Dfta.Api.Securite;

/// <summary>
/// Contrôleur pour Utilisateur.
/// </summary>
/// <param name="commands">Service injecté.</param>
/// <param name="queries">Service injecté.</param>
[ApiController]
public class UtilisateurController(IUtilisateurCommands commands, IUtilisateurQueries queries) : Controller
{
    /// <summary>
    /// Ajoute un utilisateur
    /// </summary>
    /// <param name="utilisateur">Utilisateur à sauvegarder</param>
    /// <returns>Utilisateur sauvegardé</returns>
    [HttpPost("api/utilisateurs")]
    public async Task<UtilisateurRead> AddUtilisateur([FromBody] UtilisateurWrite utilisateur)
    {
        var id = await commands.AddUtilisateur(utilisateur);
        return await queries.GetUtilisateur(id);
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
    /// Charge le détail d'un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <returns>Le détail de l'utilisateur</returns>
    [HttpGet("api/utilisateurs/{utiId:int}")]
    public async Task<UtilisateurRead> GetUtilisateur(int utiId)
    {
        return await queries.GetUtilisateur(utiId);
    }

    /// <summary>
    /// Recherche des utilisateurs
    /// </summary>
    /// <param name="nom">Nom de l'utilisateur</param>
    /// <param name="prenom">Nom de l'utilisateur</param>
    /// <param name="email">Email de l'utilisateur</param>
    /// <param name="dateNaissance">Age de l'utilisateur</param>
    /// <param name="adresse">Adresse de l'utilisateur</param>
    /// <param name="actif">Si l'utilisateur est actif</param>
    /// <param name="profilId">Profil de l'utilisateur</param>
    /// <param name="typeUtilisateurCode">Type d'utilisateur</param>
    /// <returns>Utilisateurs matchant les critères</returns>
    [HttpGet("api/utilisateurs")]
    public async Task<ICollection<UtilisateurItem>> SearchUtilisateur(string? nom = null, string? prenom = null, string? email = null, DateOnly? dateNaissance = null, string? adresse = null, bool? actif = null, int? profilId = null, TypeUtilisateur.Codes? typeUtilisateurCode = null)
    {
        return await queries.SearchUtilisateur(nom, prenom, email, dateNaissance, actif, profilId, typeUtilisateurCode);
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
        await commands.UpdateUtilisateur(utiId, utilisateur);
        return await queries.GetUtilisateur(utiId);
    }
}