namespace MinimalApiAndWasm.Api.Data;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddAppDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        
        services.AddDbContextFactory<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString, o => o.EnableRetryOnFailure());
        });

        services.AddScoped(async sp => await sp.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContextAsync());

        services.AddScoped<AppDbContextMigrator>();

        return services;
    }
}
