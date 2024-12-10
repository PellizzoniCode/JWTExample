using JWTExample.API.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTExample.API.Data;

public class BooksDbContext: DbContext
{
    public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
    {
        
    }

    public DbSet<Book> Books => Set<Book>();
}

