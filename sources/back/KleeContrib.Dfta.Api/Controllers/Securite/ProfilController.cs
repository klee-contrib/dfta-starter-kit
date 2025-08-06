////
//// ATTENTION, CE FICHIER EST PARTIELLEMENT GENERE AUTOMATIQUEMENT !
////

using KleeContrib.Dfta.Api.Models.Commands.Securite;
using KleeContrib.Dfta.Securite.Commands;
using KleeContrib.Dfta.Securite.Queries;
using KleeContrib.Dfta.Securite.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace KleeContrib.Dfta.Api.Securite;

/// <summary>
/// Profil controller
/// </summary>
/// <param name="commands">Service injecté.</param>
/// <param name="queries">Service injecté.</param>
public class ProfilController(IProfilCommands commands, IProfilQueries queries) : Controller
{
    /// <summary>
    /// Ajoute un Profil
    /// </summary>
    /// <param name="profil">Profil à sauvegarder</param>
    /// <param name="ct">CancellationToken (HttpContext.RequestAborted).</param>
    /// <returns>Profil sauvegardé</returns>
    [HttpPost("api/profils")]
    public async Task<ProfilRead> AddProfil([FromBody] ProfilWrite profil, CancellationToken ct = default)
    {
        var id = await commands.AddProfil(profil.ToProfilCommand(), ct);
        return await queries.GetProfil(id, ct);
    }

    /// <summary>
    /// Charge le détail d'un Profil
    /// </summary>
    /// <param name="proId">Id technique</param>
    /// <param name="ct">CancellationToken (HttpContext.RequestAborted).</param>
    /// <returns>Le détail de l'Profil</returns>
    [HttpGet("api/profils/{proId:int}")]
    public async Task<ProfilRead> GetProfil(int proId, CancellationToken ct = default)
    {
        return await queries.GetProfil(proId, ct);
    }

    /// <summary>
    /// Liste tous les Profils
    /// </summary>
    /// <param name="ct">CancellationToken (HttpContext.RequestAborted).</param>
    /// <returns>Profils matchant les critères</returns>
    [HttpGet("api/profils")]
    public async Task<ICollection<ProfilItem>> GetProfils(CancellationToken ct = default)
    {
        return await queries.GetProfils(ct);
    }

    /// <summary>
    /// Sauvegarde un Profil
    /// </summary>
    /// <param name="proId">Id technique</param>
    /// <param name="profil">Profil à sauvegarder</param>
    /// <param name="ct">CancellationToken (HttpContext.RequestAborted).</param>
    /// <returns>Profil sauvegardé</returns>
    [HttpPut("api/profils/{proId:int}")]
    public async Task<ProfilRead> UpdateProfil(int proId, [FromBody] ProfilWrite profil, CancellationToken ct = default)
    {
        await commands.UpdateProfil(proId, profil.ToProfilCommand(), ct);
        return await queries.GetProfil(proId, ct);
    }
}