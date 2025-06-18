using DbUp;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string? connectionString = configuration.GetConnectionString("DefaultConnection");

EnsureDatabase.For.SqlDatabase(connectionString);

var upgraderBuilder = DeployChanges.To
    .SqlDatabase(connectionString)
    .WithScriptsFromFileSystem(Path.Combine(AppContext.BaseDirectory, "Migrations"))
    .LogToConsole();

string devDataPath = Path.Combine(AppContext.BaseDirectory, "DevData");

if (Directory.Exists(devDataPath))
{
    upgraderBuilder = upgraderBuilder.WithScriptsFromFileSystem(devDataPath);
}

var upgrader = upgraderBuilder.Build();
var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();

return 0;