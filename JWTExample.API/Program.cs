using JWTExample.API.APIs;
using JWTExample.API.Data;
using JWTExample.API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<BooksDbContext>(opt => 
    opt.UseInMemoryDatabase("BooksDB"));

builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();

var booksGroup = app.MapGroup("/books")
    .RequireAuthorization();

booksGroup
    .MapGet("/", BooksEndpoints.GetAllBooks)
    .AllowAnonymous();
booksGroup
    .MapGet("/{id}", BooksEndpoints.GetBookById)
    .AllowAnonymous();

booksGroup.MapPost("/", BooksEndpoints.CreateBook);
booksGroup.MapPut("/{id}", BooksEndpoints.UpdateBook);
booksGroup.MapDelete("/{id}", BooksEndpoints.DeleteBook);

var loginGroup = app.MapGroup("/login");
var loginEndpoints = new LoginEndpoints(app.Configuration);
loginGroup
    .MapPost("/", loginEndpoints.Login)
    .AllowAnonymous();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


await app.RunAsync();