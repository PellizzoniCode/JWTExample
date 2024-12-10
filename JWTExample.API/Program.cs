using JWTExample.API.APIs;
using JWTExample.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<BooksDbContext>(opt => 
    opt.UseInMemoryDatabase("BooksDB"));


var app = builder.Build();

var booksGroup = app.MapGroup("/books");

booksGroup.MapGet("/", BooksEndpoints.GetAllBooks);
booksGroup.MapGet("/{id}", BooksEndpoints.GetBookById);
booksGroup.MapPost("/", BooksEndpoints.CreateBook);
booksGroup.MapPut("/{id}", BooksEndpoints.UpdateBook);
booksGroup.MapDelete("/{id}", BooksEndpoints.DeleteBook);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();