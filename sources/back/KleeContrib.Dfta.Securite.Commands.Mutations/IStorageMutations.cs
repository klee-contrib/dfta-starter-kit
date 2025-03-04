﻿using Kinetix.Services.Annotations;

namespace KleeContrib.Dfta.Securite.Commands.Mutations;

/// <summary>
/// Mutations pour enregistrer des fichiers sur un utilisateur.
/// </summary>
[RegisterContract]
public interface IStorageMutations
{
    /// <summary>
    /// Enregistre un fichier.
    /// </summary>
    /// <param name="baseFileName">Nom du fichier (sera complété par un Id unique).</param>
    /// <param name="file">Fichier.</param>
    /// <returns>Nom du fichier dans le storage.</returns>
    public Task<string> AddFile(string baseFileName, Stream file);

    /// <summary>
    /// Supprime un fichier.
    /// </summary>
    /// <param name="fileName">Nom du fichier à supprimer.</param>
    /// <returns>Task.</returns>
    public Task DeleteFile(string fileName);
}
