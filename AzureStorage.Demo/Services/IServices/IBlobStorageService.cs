﻿namespace AzureStorage.Demo.Services.IServices
{
    public interface IBlobStorageService
    {
        Task<string> GetBlobUrl(string imageName);
        Task RemoveBlob(string imageName);
        Task<string> UploadBlob(IFormFile formFile, string imageName, string? originalBlobName = null);
    }
}