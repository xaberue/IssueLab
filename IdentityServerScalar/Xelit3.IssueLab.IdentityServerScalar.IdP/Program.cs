using Duende.IdentityServer.Licensing;
using Xelit3.IssueLab.IdentityServerScalar;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

if (args.Contains("/seed"))
{
    SeedData.EnsureSeedData(app);

    return;
}

if (app.Environment.IsDevelopment())
{
    app.Lifetime.ApplicationStopping.Register(() =>
    {
        var usage = app.Services.GetRequiredService<LicenseUsageSummary>();
    });
}

app.MapDefaultEndpoints();

app.Run();