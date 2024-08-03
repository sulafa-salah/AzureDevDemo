using AzureCosomDb.Demo.Models;

namespace AzureCosomDb.Demo.Service
{
    public interface IToDoService
    {
        Task CreateToDo(TodoItem todoItem);
        Task DeleteToDo(string? id, string partitionKey);
        Task<List<TodoItem>> GetToDoDetails();
        Task<TodoItem> GetToDoDetailsById(string? id, string partitionKey);
    }
}