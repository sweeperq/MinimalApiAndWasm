using MinimalApiAndWasm.Api.Features.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppDataServices(builder.Configuration);

builder.Services.AddAppIdentityServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Api", policy =>
    {
        var origins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? ["https://localhost:5000"];
        policy.WithOrigins(origins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Api");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "API v1");
        options.RoutePrefix = string.Empty;
    });
}

var identityGroup = app.MapGroup("/identity").WithTags("Identity");
identityGroup.MapIdentityApi<User>();
app.MapGet("/hello", () => "Hello World!").WithTags("Hello");

await app.MigrateDbContextAsync();

app.Run();
