////
//// ATTENTION, CE FICHIER EST PARTIELLEMENT GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Securite.Commands.Models;
using KleeContrib.Dfta.Securite.Cosmmands;
using KleeContrib.Dfta.Securite.Queries;
using KleeContrib.Dfta.Securite.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace KleeContrib.Dfta.Api.Securite;

/// <summary>
/// Profil controller
/// </summary>
/// <param name="commands">Service injecté.</param>
/// <param name="queries">Service injecté.</param>
[ApiController]
public class ProfilController(IProfilCommands commands, IProfilQueries queries) : Controller
{
    /// <summary>
    /// Ajoute un Profil
    /// </summary>
    /// <param name="profil">Profil à sauvegarder</param>
    /// <returns>Profil sauvegardé</returns>
    [HttpPost("api/profils")]
    public async Task<ProfilRead> AddProfil([FromBody] ProfilWrite profil)
    {
        var id = await commands.AddProfil(profil);
        return await queries.GetProfil(id);
    }

    /// <summary>
    /// Charge le détail d'un Profil
    /// </summary>
    /// <param name="proId">Id technique</param>
    /// <returns>Le détail de l'Profil</returns>
    [HttpGet("api/profils/{proId:int}")]
    public async Task<ProfilRead> GetProfil(int proId)
    {
        return await queries.GetProfil(proId);
    }

    /// <summary>
    /// Liste tous les Profils
    /// </summary>
    /// <returns>Profils matchant les critères</returns>
    [HttpGet("api/profils")]
    public async Task<ICollection<ProfilItem>> GetProfils()
    {
        return await queries.GetProfils();
    }

    /// <summary>
    /// Sauvegarde un Profil
    /// </summary>
    /// <param name="proId">Id technique</param>
    /// <param name="profil">Profil à sauvegarder</param>
    /// <returns>Profil sauvegardé</returns>
    [HttpPut("api/profils/{proId:int}")]
    public async Task<ProfilRead> UpdateProfil(int proId, [FromBody] ProfilWrite profil)
    {
        await commands.UpdateProfil(proId, profil);
        return await queries.GetProfil(proId);
    }
}