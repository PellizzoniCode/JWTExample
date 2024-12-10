using JWTExample.API.Data;
using JWTExample.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTExample.API.APIs;

public static class BooksEndpoints
{
    public static async Task<IResult> GetAllBooks(BooksDbContext dbContext)
    {
        return Results.Ok(await dbContext.Books.ToListAsync());
    }
    
    public static async Task<IResult> GetBookById(BooksDbContext dbContext, int id)
    {
        var book = await dbContext.Books.FindAsync(id);
        return book == null ? Results.NotFound() : Results.Ok(book);
    }
    
    public static async Task<IResult> CreateBook(BooksDbContext dbContext, Book book)
    {
        await dbContext.Books.AddAsync(book);
        await dbContext.SaveChangesAsync();
        return Results.Created($"/books/{book.Id}", book);
    }
    
    public static async Task<IResult> UpdateBook(BooksDbContext dbContext, int id, Book book)
    {
        var existingBook = await dbContext.Books.FindAsync(id);
        if (existingBook == null)
        {
            return Results.NotFound();
        }
        
        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.Year = book.Year;
        
        await dbContext.SaveChangesAsync();
        return Results.Ok(existingBook);
    }
    
    public static async Task<IResult> DeleteBook(BooksDbContext dbContext, int id)
    {
        var book = await dbContext.Books.FindAsync(id);
        if (book == null)
        {
            return Results.NotFound();
        }
        
        dbContext.Books.Remove(book);
        await dbContext.SaveChangesAsync();
        return Results.NoContent();
    }
}