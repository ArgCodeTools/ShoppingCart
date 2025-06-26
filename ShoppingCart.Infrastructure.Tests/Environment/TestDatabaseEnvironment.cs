using DbUp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShoppingCart.Infrastructure.Interfaces;
using ShoppingCart.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Infrastructure.Tests.Environment;

public class TestDatabaseEnvironment : IAsyncLifetime
{
    private IServiceProvider? _serviceProvider;
    private TestConfiguration? _testConfig;

    public string ConnectionString { get; private set; } = string.Empty;
    public ISqlExecutor SqlExecutor { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        LoadConfiguration();

        ConnectionString = _testConfig!.ConnectionString;

        await SetupDatabaseAsync();
        ConfigureServices();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    private void LoadConfiguration()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: false)
            .Build();

        _testConfig = new TestConfiguration();
        configuration.GetSection("TestConfiguration").Bind(_testConfig);

        Validator.ValidateObject(_testConfig, new ValidationContext(_testConfig), validateAllProperties: true);
    }

    private async Task SetupDatabaseAsync()
    {
        try
        {
            var masterConnectionString = new SqlConnectionStringBuilder(ConnectionString)
            {
                InitialCatalog = "master"
            }.ToString();

            using var connection = new SqlConnection(masterConnectionString);
            await connection.OpenAsync();

            // Verificar si la base de datos existe de forma segura y clara
            using var checkCommand = connection.CreateCommand();
            checkCommand.CommandText = "SELECT DB_ID(@dbName)";
            checkCommand.Parameters.AddWithValue("@dbName", _testConfig!.DatabaseName);
            var result = await checkCommand.ExecuteScalarAsync();
            var databaseExists = result != DBNull.Value && result != null;

            if (databaseExists && _testConfig.RecreateDatabase)
            {
                Console.WriteLine($"Dropping existing database {_testConfig.DatabaseName}");
                using var dropCommand = connection.CreateCommand();
                dropCommand.CommandText = $@"
                ALTER DATABASE [{_testConfig.DatabaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                DROP DATABASE [{_testConfig.DatabaseName}];";
                await dropCommand.ExecuteNonQueryAsync();
                databaseExists = false;
            }

            if (!databaseExists)
            {
                using var createCommand = connection.CreateCommand();
                createCommand.CommandText = $"CREATE DATABASE [{_testConfig.DatabaseName}]";
                await createCommand.ExecuteNonQueryAsync();

                Console.WriteLine($"Database {_testConfig.DatabaseName} created successfully");
            }
            else
            {
                Console.WriteLine($"Using existing database {_testConfig.DatabaseName}");
            }

            ExecuteDbUpMigrationsAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to setup database: {ex.Message}", ex);
        }
    }


    private void ConfigureServices()
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:DefaultConnection", ConnectionString }
            })
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddScoped<ISqlExecutor, SqlExecutor>();
        services.AddLogging(builder => builder.AddConsole());

        _serviceProvider = services.BuildServiceProvider();
        SqlExecutor = _serviceProvider.GetRequiredService<ISqlExecutor>();
    }

    private void ExecuteDbUpMigrationsAsync()
    {
        try
        {
            var migrationsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _testConfig!.MigrationsPath);

            if (!Directory.Exists(migrationsPath))
                throw new DirectoryNotFoundException($"Migrations directory not found: {migrationsPath}");

            var upgraderBuilder = DeployChanges.To
                .SqlDatabase(ConnectionString)
                .WithScriptsFromFileSystem(migrationsPath)
                .LogToConsole();

            string testDataPath = Path.Combine(AppContext.BaseDirectory, "TestData");

            if (Directory.Exists(testDataPath))
            {
                upgraderBuilder = upgraderBuilder.WithScriptsFromFileSystem(testDataPath);
            }

            var result = upgraderBuilder.Build().PerformUpgrade();

            if (!result.Successful)
                throw new InvalidOperationException($"Database migration failed: {result.Error}");

            Console.WriteLine("Database migrations executed successfully");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to execute DbUp migrations: {ex.Message}", ex);
        }
    }

    public T GetService<T>() where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>()
            ?? throw new InvalidOperationException("ServiceProvider not initialized");
    }
}