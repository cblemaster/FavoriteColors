
using FavoriteColors.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configRoot = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();

string _fileDir = configRoot.GetValue<string>("file_dir") ?? "Error retreiving file directory!";
string _fileName = configRoot.GetValue<string>("file_name") ?? "Error retreiving file name!";

builder.Services.AddSingleton<IFileService>(new FileService(_fileDir, _fileName));

WebApplication app = builder.Build();

app.MapGet(pattern: "/", handler: () => "Welcome to Favorite Colors!");
app.MapGet(pattern: "/read", handler: Ok<string> (IFileService fileService) => TypedResults.Ok(fileService.TryReadFile()));
app.MapPost(pattern: "/write", handler: Results<ProblemHttpResult, Created> (IFileService fileService, string friends) =>
    fileService.TryWriteFile(friends)
        ? TypedResults.Created()
        : TypedResults.Problem("Error: an unknown error occurred while attempting to write to file."));

app.Run();
