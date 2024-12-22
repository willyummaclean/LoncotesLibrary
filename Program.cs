using Library.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Library.Models.DTOs;
using System.Runtime.ExceptionServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<LoncotesLibraryDbContext>(builder.Configuration["CreekRiverDbConnectionString"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.MapGet("/api/materials", (LoncotesLibraryDbContext db) =>
// {
//     return db.Materials.Where(material => material.OutOfCirculationSince == null)
//     .Include(m => m.MaterialType)
//     .Include(m => m.Genre)
//     .Select(m => new MaterialDTO
//     {
//         Id = m.Id,
//         MaterialName = m.MaterialName,
//         MaterialTypeId = m.MaterialTypeId,
//         MaterialType = new MaterialTypeDTO
//         {
//             Id = m.MaterialType.Id,
//             Name = m.MaterialType.Name,
//             DaysCheckedOut = m.MaterialType.DaysCheckedOut
//         },
//         GenreId = m.GenreId,
//         Genre = new GenreDTO
//         {
//             Id = m.Genre.Id,
//             Name = m.Genre.Name
//         },
//         OutOfCirculationSince = m.OutOfCirculationSince
//     }).ToList();
// });

app.MapGet("/api/materials/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    var material = db.Materials.Where(m => m.Id == id)
    .Include(m => m.MaterialType)
    .Include(m => m.Genre)
    .Include(m => m.Checkouts)
    .ThenInclude(m => m.Patron)
    .Select(m => new MaterialDTO
    {
        Id = m.Id,
        MaterialName = m.MaterialName,
        MaterialTypeId = m.MaterialTypeId,
        MaterialType = new MaterialTypeDTO
        {
            Name = m.MaterialType.Name,
            DaysCheckedOut = m.MaterialType.DaysCheckedOut
        },
        GenreId = m.GenreId,
        Genre = new GenreDTO
        {
            Name = m.Genre.Name
        },
        OutOfCirculationSince = m.OutOfCirculationSince,
        Checkouts = m.Checkouts.Select(c => new CheckoutDTO
        {
            Id = c.Id,
            PatronId = c.PatronId,
            Patron = new PatronDTO
            {
                FirstName = c.Patron.FirstName,
                LastName = c.Patron.LastName,
                Address = c.Patron.Address,
                IsActive = c.Patron.IsActive,
                Email = c.Patron.Email
            },
            CheckedOutSince = c.CheckedOutSince,
            ReturnDate = c.ReturnDate

        }).ToList()

    })
    .FirstOrDefault();


    if (material == null)
    { return Results.NotFound(); }

    return Results.Ok(material);

});

app.MapGet("/api/materials", (LoncotesLibraryDbContext db, int? genreId, int? materialTypeId) =>
{
    if (genreId == null && materialTypeId == null)
    {
        return db.Materials.Where(material => material.OutOfCirculationSince == null)
    .Include(m => m.MaterialType)
    .Include(m => m.Genre)
    .Select(m => new MaterialDTO
    {
        Id = m.Id,
        MaterialName = m.MaterialName,
        MaterialTypeId = m.MaterialTypeId,
        MaterialType = new MaterialTypeDTO
        {
            Id = m.MaterialType.Id,
            Name = m.MaterialType.Name,
            DaysCheckedOut = m.MaterialType.DaysCheckedOut
        },
        GenreId = m.GenreId,
        Genre = new GenreDTO
        {
            Id = m.Genre.Id,
            Name = m.Genre.Name
        },
        OutOfCirculationSince = m.OutOfCirculationSince
    }).ToList();

    }
    else if (genreId == null && materialTypeId.HasValue == true)
    {
        return db.Materials.Where(material => material.OutOfCirculationSince == null)
    .Where(material => material.MaterialTypeId == materialTypeId)
    .Include(m => m.MaterialType)
    .Include(m => m.Genre)
    .Select(m => new MaterialDTO
    {
        Id = m.Id,
        MaterialName = m.MaterialName,
        MaterialTypeId = m.MaterialTypeId,
        MaterialType = new MaterialTypeDTO
        {
            Id = m.MaterialType.Id,
            Name = m.MaterialType.Name,
            DaysCheckedOut = m.MaterialType.DaysCheckedOut
        },
        GenreId = m.GenreId,
        Genre = new GenreDTO
        {
            Id = m.Genre.Id,
            Name = m.Genre.Name
        },
        OutOfCirculationSince = m.OutOfCirculationSince
    }).ToList();
    }
    else if (genreId.HasValue == true && materialTypeId == null)
    {
        return db.Materials.Where(material => material.OutOfCirculationSince == null)
    .Where(material => material.GenreId == genreId)
    .Include(m => m.MaterialType)
    .Include(m => m.Genre)
    .Select(m => new MaterialDTO
    {
        Id = m.Id,
        MaterialName = m.MaterialName,
        MaterialTypeId = m.MaterialTypeId,
        MaterialType = new MaterialTypeDTO
        {
            Id = m.MaterialType.Id,
            Name = m.MaterialType.Name,
            DaysCheckedOut = m.MaterialType.DaysCheckedOut
        },
        GenreId = m.GenreId,
        Genre = new GenreDTO
        {
            Id = m.Genre.Id,
            Name = m.Genre.Name
        },
        OutOfCirculationSince = m.OutOfCirculationSince
    }).ToList();
    }
    else
    {
        return db.Materials.Where(material => material.OutOfCirculationSince == null)
    .Where(material => material.GenreId == genreId)
    .Where(material => material.MaterialTypeId == materialTypeId)
    .Include(m => m.MaterialType)
    .Include(m => m.Genre)
    .Select(m => new MaterialDTO
    {
        Id = m.Id,
        MaterialName = m.MaterialName,
        MaterialTypeId = m.MaterialTypeId,
        MaterialType = new MaterialTypeDTO
        {
            Id = m.MaterialType.Id,
            Name = m.MaterialType.Name,
            DaysCheckedOut = m.MaterialType.DaysCheckedOut
        },
        GenreId = m.GenreId,
        Genre = new GenreDTO
        {
            Id = m.Genre.Id,
            Name = m.Genre.Name
        },
        OutOfCirculationSince = m.OutOfCirculationSince
    }).ToList();
    }

});





app.Run();


