using Azure.Storage.Blobs;

namespace AzureClassLibrary.Storage;

public class Blob
{
    private readonly string _connectionString;
    
    public Blob()
    {
        _connectionString = Environment.GetEnvironmentVariable("AZURE_BLOB_CONN_STRING") ?? "";
    }

    public int Demo()
    {
        // Get Blob Client
        BlobServiceClient blobServiceClient = new BlobServiceClient(this._connectionString);
        // Create a unique name for the container
        string containerName = "quickstartblobs" + Guid.NewGuid().ToString();
        // Create the container and return a container client object
        blobServiceClient.CreateBlobContainer(containerName);

        return 0;
    }
}