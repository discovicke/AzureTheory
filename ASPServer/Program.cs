using System.ComponentModel;
using System.Runtime.CompilerServices;
using Azure.Storage.Blobs;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AzureStorage");

builder.Services.AddSingleton(new BlobServiceClient(connectionString));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/upload/{id}", async (BlobServiceClient blobClient, string id) =>
{
    // använd samma namn som upload container 
    var containerName = "useruploads";
    var containerClient = blobClient.GetBlobContainerClient(containerName);

    if (!await containerClient.ExistsAsync())
    {
        return Results.NotFound();
    }

    // id här är blob namnet
    var blobClientForDownload = containerClient.GetBlobClient(id);
    if (!await blobClientForDownload.ExistsAsync())
    {
        return Results.NotFound();
    }

    var (contentStream, contentType) = await DownloadBlobAsync(blobClientForDownload);

    return Results.File(contentStream, contentType, id);
});
app.MapPost(
    "/upload",
    async (BlobServiceClient blobServiceClient, IFormFile file) =>
    {
        if (file == null || file.Length == 0)
        {
            return Results.BadRequest("No file");
        }

        var containerName = "useruploads"; //lägg till!
        var blobName = file.FileName;

        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(blobName);

        using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream, overwrite: true);

        return Results.Ok(new { blobName });
    }
);
app.Run();

static async Task<(Stream Content, string ContentType)> DownloadBlobAsync(BlobClient blobClient)
{
    var response = await blobClient.DownloadStreamingAsync();
    var contentType = response.Value.Details.ContentType ?? "application/octet-stream";
    return (response.Value.Content, contentType);
}

static BlobServiceClient GetBlobServiceClient(string accountName)
{
    BlobServiceClient client = new(
        new Uri($"https://{accountName}.blob.core.windows.net"),
        new DefaultAzureCredential());

    return client;
}