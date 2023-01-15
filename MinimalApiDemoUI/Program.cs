using DataAcess.Data;
using DataAcess.Models;
using DataAcess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<PersonContext>();

builder.Services.AddTransient<IPersonRepository, PersonRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("api/Person", async (IPersonRepository personRepo) =>
{
    var people = await personRepo.GetAll();
    return Results.Ok(people);
});

app.MapPost("api/Person", async (IPersonRepository personRepo, Person person) =>
{
    var result = await personRepo.Add(person);
    if (result)
        return Results.Ok("Person save succesfully");
    return Results.Problem();
});

app.MapPut("api/Person", async (IPersonRepository personRepo, Person person) =>
{
    var result = await personRepo.Update(person);
    if (result)
        return Results.Ok("Person updated succesfully");
    return Results.Problem();
});

app.MapGet("api/Person/{id}", async (IPersonRepository personRepo,int id) =>
{
    var person = await personRepo.GetById(id);
    if (person is null)
        return Results.NotFound();
    return Results.Ok(person);
});

app.MapDelete("api/Person/{id}", async (IPersonRepository personRepo, int id) =>
{
    var result = await personRepo.Delete(id);
    if (result)
        return Results.Ok("Person deleted succesfully");
    return Results.Problem();
});

app.Run();
