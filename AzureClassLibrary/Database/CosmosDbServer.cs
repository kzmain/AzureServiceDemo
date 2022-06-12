using Application_Insight_MVC.Controllers;
using Microsoft.Azure.Cosmos;

namespace AzureClassLibrary.Database;

public class CosmosDbServer
{
    public CosmosDbServer()
    {
    }
    
    public int Demo()
    {
        string endpointUrl = Environment.GetEnvironmentVariable("COSMOS_DB_ENDPOINT") ?? ""; 
        string primaryKey  = Environment.GetEnvironmentVariable("COSMOS_DB_PRIMARY_KEY") ?? ""; 
        string databaseId  = Environment.GetEnvironmentVariable("COSMOS_DB_DATABASE_ID") ?? ""; 
        string containerId = Environment.GetEnvironmentVariable("COSMOS_DB_CONTAINER_ID") ?? "";

        CosmosClient cosmosClient = new CosmosClient(endpointUrl, primaryKey);
        Container container = cosmosClient.GetContainer(databaseId, containerId);
        
        var sqlQueryText = "SELECT * FROM c";
        
        QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);

        FeedIterator<ToDoItemClass.ToDoItem> queryResultSetIterator = container.GetItemQueryIterator<ToDoItemClass.ToDoItem>(queryDefinition);
        List<ToDoItemClass.ToDoItem> toDoItems = new List<ToDoItemClass.ToDoItem>();

        while (queryResultSetIterator.HasMoreResults)
        {
            FeedResponse<ToDoItemClass.ToDoItem> currentResultSet = queryResultSetIterator.ReadNextAsync().Result;
            foreach (ToDoItemClass.ToDoItem toDoItem in currentResultSet)
            {
                toDoItems.Add(toDoItem);
                Console.WriteLine("\tRead {0}\n", toDoItem);
            }
        }

        return 0;
    }
}