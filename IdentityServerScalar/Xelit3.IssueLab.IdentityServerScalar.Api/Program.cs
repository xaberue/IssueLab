using Xelit3.IssueLab.IdentityServerScalar.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddCustomOpenApi();

var jwtAuthSettings = builder.Configuration.GetSection("Auth").Get<JwtAuthSettings>()
    ?? throw new ArgumentNullException("JWT Auth Settings must be configured");

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = jwtAuthSettings.Authority;
        options.TokenValidationParameters.ValidateAudience = true;
        options.Audience = jwtAuthSettings.Audience;
        options.RequireHttpsMetadata = builder.Environment.IsDevelopment() ? false : true;
    });

builder.Services
    .AddAuthorization(options =>
    {
        options.AddPolicy("ReadAccessPolicy", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("scope", "api.read");
        });
    });

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.RequireAuthorization("ReadAccessPolicy")
.WithName("GetWeatherForecast");

app.ConfigureOpenApiWithScalarUI();

app.MapDefaultEndpoints();

app.Run();