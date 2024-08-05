using Azure.Data.Tables;
using Azure;
using AzureStorage.Demo.Data;
using AzureStorage.Demo.Services.IServices;

namespace AzureStorage.Demo.Services
{
    public class TableStorageService : ITableStorageService
    {
        private const string tableName = "Attendees";
        private readonly IConfiguration _configuration;
        private readonly TableServiceClient _tableServiceClient;
        private readonly TableClient _tableClient;
        public TableStorageService(IConfiguration configuration, TableServiceClient tableServiceClient, TableClient tableClient)
        {
            this._configuration = configuration;
            this._tableServiceClient = tableServiceClient;
            this._tableClient = tableClient;
        }

        public async Task<AttendeeEntity> GetAttendee(string industry, string id)
        {
            
            return await _tableClient.GetEntityAsync<AttendeeEntity>(industry, id);
        }

        public async Task<List<AttendeeEntity>> GetAttendees()
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);
            Pageable<AttendeeEntity> attendeeEntities = _tableClient.Query<AttendeeEntity>();
            return attendeeEntities.ToList();
        }

        public async Task UpsertAttendee(AttendeeEntity attendeeEntity)
        {
          
            await _tableClient.UpsertEntityAsync(attendeeEntity);
        }

        public async Task DeleteAttendee(string industry, string id)
        {
           
            await _tableClient.DeleteEntityAsync(industry, id);
        }

        private async Task<TableClient> GetTableClient()
        {
            var tableClient = _tableServiceClient.GetTableClient(tableName);
            await tableClient.CreateIfNotExistsAsync();

            return tableClient;
        }
    }
}