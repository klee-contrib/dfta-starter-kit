﻿using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Models;

namespace KleeContrib.Dfta.Securite.Commands.Mutations;

/// <summary>
/// Mutations pour les utilisateurs.
/// </summary>
[RegisterContract]
public interface IUtilisateurMutations
{
    /// <summary>
    /// Ajoute un utilisateur
    /// </summary>
    /// <param name="utilisateur">Utilisateur à sauvegarder</param>
    /// <returns>Utilisateur sauvegardé</returns>
    Task<int> AddUtilisateur(UtilisateurCommand utilisateur);

    /// <summary>
    /// Supprime un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <returns>Task.</returns>
    Task DeleteUtilisateur(int utiId);

    /// <summary>
    /// Sauvegarde un utilisateur
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <param name="utilisateur">Utilisateur à sauvegarder</param>
    /// <returns>Utilisateur sauvegardé</returns>
    Task UpdateUtilisateur(int utiId, UtilisateurCommand utilisateur);

    /// <summary>
    /// Met à jour le nom du fichier de photo d'un utilisateur.
    /// </summary>
    /// <param name="utiId">Id de l'utilisateur</param>
    /// <param name="fileName">Nom du fichier.</param>
    /// <returns>Task.</returns>
    Task UpdateUtilisateurPhotoFileName(int utiId, string? fileName);
}
