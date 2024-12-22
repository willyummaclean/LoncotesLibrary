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
    // Validation checks
    if (genreId.HasValue && !db.Genres.Any(g => g.Id == genreId.Value))
    {
        return Results.BadRequest($"Genre with Id {genreId.Value} does not exist.");
    }

    if (materialTypeId.HasValue && !db.MaterialTypes.Any(mt => mt.Id == materialTypeId.Value))
    {
        return Results.BadRequest($"MaterialType with Id {materialTypeId.Value} does not exist.");
    }

    // Query materials with optional filtering
    var query = db.Materials
        .Where(m => m.OutOfCirculationSince == null) // Only in-circulation materials
        .Include(m => m.MaterialType)
        .Include(m => m.Genre)
        .AsQueryable();

    if (genreId.HasValue)
    {
        query = query.Where(m => m.GenreId == genreId.Value);
    }

    if (materialTypeId.HasValue)
    {
        query = query.Where(m => m.MaterialTypeId == materialTypeId.Value);
    }

    var materials = query.Select(m => new MaterialDTO
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

    return Results.Ok(materials);
});

app.MapPost("/api/materials", (LoncotesLibraryDbContext db, Material material) =>
{
    db.Materials.Add(material);
    db.SaveChanges();
    return Results.Created($"/api/campsites/{material.Id}", material);
});

app.MapDelete("/api/materials/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    Material material = db.Materials.SingleOrDefault(m => m.Id == id);
    if (material == null)
    {
        return Results.NotFound();
    }
    material.OutOfCirculationSince = DateTime.Now;
    db.SaveChanges();
    return Results.NoContent();

});

app.MapGet("/api/genres", (LoncotesLibraryDbContext db) =>
{
    return db.Genres
    .Select(g => new GenreDTO
    {
        Id = g.Id,
        Name = g.Name
    }).ToList();
});

app.MapGet("/api/materialtypes", (LoncotesLibraryDbContext db) =>
{
    return db.MaterialTypes
    .Select(m => new MaterialTypeDTO
    {
        Id = m.Id,
        Name = m.Name,
        DaysCheckedOut = m.DaysCheckedOut
    }).ToList();
});

app.MapGet("/api/patrons", (LoncotesLibraryDbContext db) =>
{
    return db.Patrons
    .Select(p => new PatronDTO
    {
        Id = p.Id,
        FirstName = p.FirstName,
        LastName = p.LastName,
        Address = p.Address,
        IsActive = p.IsActive,
        Email = p.Email
    }).ToList();
});

app.MapPost("/api/checkouts", (LoncotesLibraryDbContext db, Checkout checkout) =>
{
    checkout.CheckedOutSince = DateTime.Today;
    db.Checkouts.Add(checkout);
    db.SaveChanges();
    return Results.Created($"/api/checkouts/{checkout.Id}", checkout);
});

app.MapGet("api/patrons/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    var patron = db.Patrons.Where(p => p.Id == id)
    .Select(p => new PatronDTO
    {
        Id = p.Id,
        FirstName = p.FirstName,
        LastName = p.LastName,
        Address = p.Address,
        IsActive = p.IsActive,
        Email = p.Email
    });

    if (patron == null)
    {
        return Results.NotFound($"Patron with Id {id} not found.");
    }

    var checkouts = db.Checkouts.Where(c => c.PatronId == id)
    .Include(c => c.Material)
    .ThenInclude(m => m.MaterialType)
    .Select(c => new CheckoutDTO()
    {
        Id = c.Id,
        MaterialId = c.MaterialId,
        PatronId = c.PatronId,
        CheckedOutSince = c.CheckedOutSince,
        ReturnDate = c.ReturnDate,
        Material = new MaterialDTO()
        {
            Id = c.Material.Id,
            MaterialName = c.Material.MaterialName,
            MaterialTypeId = c.Material.MaterialTypeId,
            MaterialType = new MaterialTypeDTO()
            {
                Id = c.Material.MaterialType.Id,
                Name = c.Material.MaterialType.Name
            },
            GenreId = c.Material.GenreId,
            OutOfCirculationSince = c.Material.OutOfCirculationSince
        }
    }).ToList();

    var result = new
    {
        Patron = patron,
        Checkouts = checkouts
    };

    return Results.Ok(result);
});

app.MapPut("/api/checkouts", (LoncotesLibraryDbContext db, Checkout checkout) =>
{
    checkout.ReturnDate = DateTime.Today;
    db.Checkouts.Add(checkout);
    db.SaveChanges();
    return Results.Created($"/api/checkouts/{checkout.Id}", checkout);
});

app.MapPut("api/patrons/{id}", (LoncotesLibraryDbContext db, int id, string? email, string? address) =>
{
    Patron patron = db.Patrons.Where(p => p.Id == id).FirstOrDefault();
    if (patron == null)
    {
        return Results.NotFound($"Patron with Id {id} not found.");
    }
    if (email != null)
    {
        patron.Email = email;
    }
    if (address != null)
    {
        patron.Address = address;
    }
    db.SaveChanges();
    return Results.Ok();

});

app.MapDelete("api/patrons/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    Patron patron = db.Patrons.Where(p => p.Id == id).FirstOrDefault();
    if (patron == null)
    {
        return Results.NotFound($"Patron with Id {id} not found.");
    }
    else
    {
        patron.IsActive = false;
        return Results.Ok();
    }


});

app.Run();


