namespace MinimalApiAndWasm.Api.Data;

public static class WebApplicationExtensions
{
    public static async Task<WebApplication> MigrateDbContextAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var migrator = scope.ServiceProvider.GetRequiredService<AppDbContextMigrator>();
        await migrator.MigrateAsync();
        return app;
    }
}
