using AzureCosomDb.Demo.Models;
using Microsoft.Azure.Cosmos;

namespace AzureCosomDb.Demo.Service
{
    public class ToDoService : IToDoService  
    {
        private readonly string CosmosDbConnectionString = "";
        private readonly string CosmosDbKey = "";
        private readonly string CosmosDbName = "";
        private readonly string CosmosDbContainerName = "";


        //private Container GetContainerClient()
        //{ 
        //    var cosmosDbClient = new CosmosClient(CosmosDbConnectionString);
        //     var container = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
        //    return container;
        //}

        private readonly Container container;

        public ToDoService(Container container)
        {
            this.container = container;
        }
        public async Task CreateToDo(TodoItem todoItem)
        {
            try
            {
               
                
                var updateRes = await container.CreateItemAsync(todoItem, new PartitionKey(todoItem.Category));
                
            }
            catch (Exception ex)
            {
                throw new Exception("Exception", ex);
            }
        }

        public async Task DeleteToDo(string? id, string partitionKey)
        {

            try
            {
               
                var response = await container.DeleteItemAsync<TodoItem>(id, new PartitionKey(partitionKey));
            }
          
            catch (Exception ex)
            {
                throw new Exception("Exception", ex);
            }
        }

        public async Task<List<TodoItem>> GetToDoDetails()
        {
            List<TodoItem> todos = new List<TodoItem>();
            try
            {
               
                var sqlQuery = "SELECT * FROM c";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
             
                FeedIterator<TodoItem> queryResultSetIterator = container.GetItemQueryIterator<TodoItem>(queryDefinition);

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<TodoItem> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (TodoItem item in currentResultSet)
                    {
                        todos.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
            return todos;
        }

        public async Task<TodoItem> GetToDoDetailsById(string? id, string partitionKey)
        {
            try
            {
              
                ItemResponse<TodoItem> response = await container.ReadItemAsync<TodoItem>(id, new PartitionKey(partitionKey));
                return response.Resource;
            }
             
    catch (CosmosException ex) when(ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            // Handle not found error, possibly return null or custom error
            return null;
        }
            catch (Exception ex)
            {

                throw new Exception("Exception ", ex);
            }
        }
    }
}