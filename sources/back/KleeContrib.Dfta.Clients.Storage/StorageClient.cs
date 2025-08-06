using Azure;
using Azure.Storage.Blobs;
using Kinetix.Services.Annotations;
using KleeContrib.Dfta.Securite.Commands.Mutations;
using KleeContrib.Dfta.Securite.Queries;

namespace KleeContrib.Dfta.Clients.Storage;

/// <summary>
/// Implémentation des commandes et queries de storage.
/// </summary>
[RegisterImpl]
public class StorageClient(BlobServiceClient client) : IStorageQueries, IStorageMutations
{
    private BlobContainerClient Container => client.GetBlobContainerClient("files");

    /// <inheritdoc cref="IStorageMutations.AddFile" />
    public async Task<string> AddFile(string baseFileName, Stream file, CancellationToken ct = default)
    {
        var fileName = $"{baseFileName}-{Guid.NewGuid()}";
        await Container.UploadBlobAsync(fileName, file, ct);
        return fileName;
    }

    /// <inheritdoc cref="IStorageMutations.DeleteFile" />
    public async Task DeleteFile(string fileName, CancellationToken ct = default)
    {
        await Container.GetBlobClient(fileName).DeleteIfExistsAsync(cancellationToken: ct);
    }

    /// <inheritdoc cref="IStorageQueries.GetFile" />
    public async Task<byte[]?> GetFile(string fileName, CancellationToken ct = default)
    {
        try
        {
            var content = await Container.GetBlobClient(fileName).DownloadContentAsync(ct);
            return content.Value.Content.ToArray();
        }
        catch (RequestFailedException e) when (e.ErrorCode == "BlobNotFound")
        {
            return null;
        }
    }
}
