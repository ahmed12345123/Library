using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AutoMapper;
using library.Models;
using library.Dtos;



namespace library.Mapping
{
    public class BooksProfiles
    {
        public class BooksProfile : Profile
        {
            public BooksProfile()
            {
                CreateMap<Book, BookReadDto>();
                CreateMap<BookCreateDto, Book>();
                CreateMap<BookUpdateDto, Book>();
                CreateMap<Book, BookUpdateDto>();
            }

        }
    }
}
