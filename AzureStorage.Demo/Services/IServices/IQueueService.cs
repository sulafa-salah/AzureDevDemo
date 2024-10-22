using AzureStorage.Demo.Models;

namespace AzureStorage.Demo.Services.IServices
{
    public interface IQueueService
    {
        Task SendMessage(EmailMessage emailMessage);
    }
}