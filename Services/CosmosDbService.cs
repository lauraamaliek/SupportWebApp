using Microsoft.Azure.Cosmos;
using SupportWebApp.Models;

namespace SupportWebApp.Services;

public class CosmosDbService
{
    private readonly CosmosClient _cosmosClient;
    private readonly Container _container;

    public CosmosDbService(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("CosmosDb");
        string databaseName = configuration["CosmosDb:DatabaseName"];
        string containerName = configuration["CosmosDb:ContainerName"];

        _cosmosClient = new CosmosClient(connectionString);
        _container = _cosmosClient.GetContainer(databaseName, containerName);
    }

    public async Task CreateSupportMessageAsync(SupportMessage message)
    {
        await _container.CreateItemAsync(
            item: message,
            partitionKey: new PartitionKey(message.Category));
    }
    
    public async Task<List<SupportMessage>> GetAllSupportMessagesAsync()
    {
        var results = new List<SupportMessage>();

        using FeedIterator<SupportMessage> iterator = _container.GetItemQueryIterator<SupportMessage>(
            queryText: "SELECT * FROM c ORDER BY c.dateTime DESC");

        while (iterator.HasMoreResults)
        {
            FeedResponse<SupportMessage> response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }
}