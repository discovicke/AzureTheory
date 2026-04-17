using System.ComponentModel;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration(
    "DefaultEndpointsProtocol=https;AccountName=blobtester;AccountKey=FtFjO9g0WEV8SOJyI0rN5kZ5t2uRBTNRelbgNzCDJ0OW1Mk2jSMQz5aO+85I91KDl6ud8f75/pn7+AStrpzbKQ==;EndpointSuffix=core.windows.net"
); //lägg till

builder.Services.AddSingleton(new BlobServiceClient(connectionString));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/upload/{id}", async (BlobServiceClient blobServiceClient, string id) =>
{
    var blobClient = blobServiceClient.GetBlobContainerClient(id);

    if (!await blobClient.ExistsAsync())
    {
        return Results.NotFound();
    }

    var response = await blobClient.DownloadAsync();

    var contentType = response.Value.Details.ContentType ?? "application/octet-stream";



    return Results.File(response.Value.Content, contentType, id);

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
