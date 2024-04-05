using System;
using library.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace library.Persistence
{
    
        public class LibraryDbContext : DbContext
        {
            public LibraryDbContext(DbContextOptions<LibraryDbContext> opt) : base(opt)
            {

            }

            public DbSet<Book> Books { get; set; }

        }
    
}
