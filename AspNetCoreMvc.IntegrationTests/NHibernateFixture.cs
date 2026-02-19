using Data.Configuration;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreMvc.IntegrationTests;

public class NHibernateFixture : IDisposable
{
    public NHibernateFixture()
    {
        // Build configuration to read connection string
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Testing.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.Testing.json");
        }

        // Read ShowSql setting (default to false for cleaner test output)
        var showSql = configuration.GetValue<bool>("NHibernate:ShowSql", false);

        // Initialize NHibernate SessionFactory once for all tests
        NHibernateHelper.InitSessionFactory(connectionString, showSql);
    }

    public void Dispose()
    {
        // Cleanup if needed
    }
}

[CollectionDefinition("NHibernate Collection")]
public class NHibernateCollection : ICollectionFixture<NHibernateFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
