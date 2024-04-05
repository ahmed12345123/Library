using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Collections.Generic;
using library.Core;
using library.Dtos;
using library.Models;
using library.Mapping;
using library.Persistence;
//using static library.Core.IUnitOfWork;

namespace library.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepo _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IBookRepo repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //GET api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadDto>>> GetAllBooks()
        {
            var bookItems = await _repository.GetAllBooks();

            return Ok(_mapper.Map<IEnumerable<BookReadDto>>(bookItems));
        }

        //GET api/books/{id}
        [HttpGet("{id}", Name = "GetBookById")]
        public async Task<ActionResult<BookReadDto>> GetBookById(int id)
        {
            var bookItem = await _repository.GetBookById(id);
            if (bookItem != null)
            {
                return Ok(_mapper.Map<BookReadDto>(bookItem));
            }
            return NotFound();
        }

        //POST api/books
        [HttpPost]
        public async Task<ActionResult<BookReadDto>> CreateBook(BookCreateDto BookCreateDto)
        {
            var bookModel = _mapper.Map<Book>(BookCreateDto);
            _repository.CreateBook(bookModel);
            await _unitOfWork.CompleteAsync();
            


            var BookReadDto = _mapper.Map<BookReadDto>(bookModel);

            return CreatedAtRoute(nameof(GetBookById), new { Id = BookReadDto.Id }, BookReadDto);
        }

        //PUT api/books/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, BookUpdateDto BookUpdateDto)
        {
            var bookModelFromRepo = await _repository.GetBookById(id);
            if (bookModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(BookUpdateDto, bookModelFromRepo);

            _repository.UpdateBook(bookModelFromRepo);

            
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        //PATCH api/books/{id}
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialBookUpdate(int id, JsonPatchDocument<BookUpdateDto> patchDoc)
        {
            var bookModelFromRepo = await _repository.GetBookById(id);
            if (bookModelFromRepo == null)
            {
                return NotFound();
            }

            var bookToPatch = _mapper.Map<BookUpdateDto>(bookModelFromRepo);
            patchDoc.ApplyTo(bookToPatch, ModelState);

            if (!TryValidateModel(bookToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(bookToPatch, bookModelFromRepo);

            _repository.UpdateBook(bookModelFromRepo);

            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        //DELETE api/books/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var bookModelFromRepo = await _repository.GetBookById(id);
            if (bookModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteBook(bookModelFromRepo);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

    }
}
