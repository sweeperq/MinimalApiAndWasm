namespace MinimalApiAndWasm.Api.Data;

public class AppDbContextMigrator(IDbContextFactory<AppDbContext> dbContextFactory)
{
    /// <summary>
    /// Migrate database and seed initial data.
    /// </summary>
    /// <param name="seedSampleData">Flag indicating whether to seed sample data for demo/testing purposes. (Default: false)</param>
    /// <param name="resetDatabase">Flat indicating whether to reset the database. (Default: false)</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task MigrateAsync(bool seedSampleData = false, bool resetDatabase = false, CancellationToken cancellationToken = default)
    {
        await using var db = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        if (db.Database.IsRelational())
        {
            if (resetDatabase)
            {
                await db.Database.EnsureDeletedAsync(cancellationToken);
            }
            await db.Database.MigrateAsync(cancellationToken);
        }

        await SeedAsync(cancellationToken);

        if (seedSampleData)
        {
            await SeedSampleDataAsync(cancellationToken);
        }
    }

    /// <summary>
    /// Seed initial required data after migration.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task SeedAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Seed sample data for demo or testing purpose
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual Task SeedSampleDataAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
