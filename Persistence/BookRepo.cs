using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using library.Core;
using library.Models;
using library.Mapping;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace library.Persistence
{
    public class BookRepo : IBookRepo
    {
        
            private readonly LibraryDbContext _context;

            public BookRepo(LibraryDbContext context)
            {
                _context = context;
            }

            public void CreateBook(Book book)
            {
                if (book == null)
                {
                    throw new ArgumentNullException(nameof(book));
                }

                _context.Books.Add(book);
            }

            public void DeleteBook(Book book)
            {
                if (book == null)
                {
                    throw new ArgumentNullException(nameof(book));
                }
                 _context.Books.Remove(book);
            }

            public async Task<IEnumerable<Book>> GetAllBooks()
            {
                return await _context.Books.ToListAsync();
            }

            public async Task<Book> GetBookById(int id)
            {
                return await _context.Books.FirstOrDefaultAsync(p => p.Id == id);
            }

            
            public void UpdateBook(Book book)
            {
                //Nothing
            }
     }
}

