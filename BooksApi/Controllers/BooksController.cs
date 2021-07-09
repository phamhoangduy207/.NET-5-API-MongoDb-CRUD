using AutoMapper;
using BooksApi.Models;
using BooksApi.Services;
using BooksApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly ILogger<BooksController> logger;
        private readonly IMapper mapper;

        public BooksController(BookService bookService, IMapper mapper, ILogger<BooksController> logger)
        {
            this._bookService = bookService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        /*         public ActionResult<List<Book>> Get() =>
                    _bookService.Get(); */
        public async Task<IEnumerable<BookViewModel>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            var result = mapper.Map<List<Book>, IEnumerable<BookViewModel>>(books);
            return result;
        }


        [HttpGet("{id:length(24)}", Name = "GetBook")]
        /* public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        } */
        public async Task<IActionResult> GetBookById(string id)
        {
            var foundBook = await _bookService.GetBookById(id);
            if (foundBook == null)
            {
                return BadRequest();
            }
            return Ok(mapper.Map<BookViewModel>(foundBook));
        }
        [HttpPost]
        /* public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        } */
        public async Task<IActionResult> CreateBook([FromBody] Book newBookDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var isCreated = await _bookService.CreateBook(newBookDetails);

            if (isCreated == false)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut("{id:length(24)}")]
        /* public IActionResult Update(Book bookIn)
        {
            _bookService.Update(bookIn);
            return NoContent();
        } */
        public async Task<IActionResult> UpdateBook(string id, [FromBody] Book book)
        {
            var isUpdated = await _bookService.UpdateBook(id, book);
            if (isUpdated == false)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{id:length(24)}")]
        /* public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        } */
        public async Task<ActionResult> DeleteBook(string id)
        {
            var isDeleted = await _bookService.DeleteBook(id);
            if (isDeleted == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}