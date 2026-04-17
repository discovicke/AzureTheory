using System.ComponentModel;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration("");//lägg till

builder.Services.AddSingleton(new BlobServiceClient(connectionString));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/", () => "Hello World!");

app.MapPost("/upload", async (BlobServiceClient blobServiceClient, IFormFile file) =>
{
    if (file == null || file.Length == 0)
    {
        return Results.BadRequest("No file");
    }

    var containerName = ""; //lägg till!
    var blobName = file.FileName;

    var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    await containerClient.CreateIfNotExistsAsync();

    var blobClient = containerClient.GetBlobClient(blobName);

    using var stream = file.OpenReadStream();
    await blobClient.UploadAsync(stream, overwrite: true);

    return Results.Ok(new { blobName });

});

app.Run();
