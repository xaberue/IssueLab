using Bogus;
using IssueLab.Blazor.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/users/all", () =>
{
    var userFaker = new Faker<UserViewModel>()        
        .RuleFor(u => u.Id, Guid.NewGuid())
        .RuleFor(u => u.FullName, f => f.Person.FullName)
        .RuleFor(u => u.Email, f => f.Person.Email)
        .RuleFor(u => u.BirthDate, f => f.Person.DateOfBirth)
        .RuleFor(u => u.Status, f => (AccountStatus)f.Random.Int(1, 3));

    var fakeUsers = userFaker.Generate(200);
    
    return fakeUsers;
})
.WithName("GetAllUsers")
.WithOpenApi();

app.Run();
