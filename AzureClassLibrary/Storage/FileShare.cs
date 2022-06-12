using Azure.Storage.Files.Shares;

namespace AzureClassLibrary.Storage;

public class FileShare
{
    private readonly string _connectionString;
    
    public FileShare()
    {
        _connectionString = Environment.GetEnvironmentVariable("AZURE_FILE_SHARE_CONN_STRING") ?? "";
    }

    public int Demo()
    {
        // Create a unique name for the file share name
        string fileShareName = "quickstartfileshare" + Guid.NewGuid().ToString();
        // Instantiate a ShareClient which will be used to create and manipulate the file share
        ShareClient share = new ShareClient(this._connectionString, fileShareName);
        // Create the share if it doesn't already exist
        share.CreateIfNotExists();
        // Create the directory and return a directory client object
        ShareDirectoryClient directory = share.GetDirectoryClient("CustomLogs");
        directory.CreateIfNotExists();
        return 0;
    }
}