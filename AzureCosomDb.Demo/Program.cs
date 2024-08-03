
using AzureCosomDb.Demo.Service;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Cosmos DB configuration
var cosmosDbEndpoint = "https://localhost:8081/";
var cosmosDbKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
var databaseName = "ToDoDatabase";
var containerName = "ToDO";



builder.Services.AddSingleton<CosmosClient>(serviceProvider =>
{
    return new CosmosClient(cosmosDbEndpoint, cosmosDbKey);
});

builder.Services.AddSingleton<IToDoService>(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<CosmosClient>();
    var container = client.GetContainer(databaseName, containerName);
    return new ToDoService(container);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
