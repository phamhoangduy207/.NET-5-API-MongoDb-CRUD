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
    public class ChaptersController : ControllerBase
    {
        private readonly ChapterService _chapterService;
        private readonly ILogger<ChaptersController> logger;
        private readonly IMapper mapper;

        public ChaptersController(ChapterService chapterService, IMapper mapper, ILogger<ChaptersController> logger)
        {
            this._chapterService = chapterService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        /*         public ActionResult<List<Book>> Get() =>
                    _chapterService.Get(); */
        public async Task<IEnumerable<ChapterViewModel>> GetAllChapters()
        {
            var books = await _chapterService.GetAllChapters();
            var result = mapper.Map<List<Chapter>, IEnumerable<ChapterViewModel>>(books);
            return result;
        }


        [HttpGet("{id:length(24)}", Name = "GetChapter")]
        /* public ActionResult<Book> Get(string id)
        {
            var book = _chapterService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        } */
        public async Task<IActionResult> GetChapterById(string id)
        {
            var foundBook = await _chapterService.GetChapterById(id);
            if (foundBook == null)
            {
                return BadRequest();
            }
            return Ok(mapper.Map<ChapterViewModel>(foundBook));
        }
        [HttpPost]
        /* public ActionResult<Book> Create(Book book)
        {
            _chapterService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        } */
        public async Task<IActionResult> CreateChapter([FromBody] Chapter newBookDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var isCreated = await _chapterService.CreateChapter(newBookDetails);

            if (isCreated == false)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPut("{id:length(24)}")]
        /* public IActionResult Update(Book bookIn)
        {
            _chapterService.Update(bookIn);
            return NoContent();
        } */
        public async Task<IActionResult> UpdateChapter(string id, [FromBody] Chapter book)
        {
            var isUpdated = await _chapterService.UpdateChapter(id, book);
            if (isUpdated == false)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("{id:length(24)}")]
        /* public IActionResult Delete(string id)
        {
            var book = _chapterService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _chapterService.Remove(book.Id);

            return NoContent();
        } */
        public async Task<ActionResult> DeleteChapter(string id)
        {
            var isDeleted = await _chapterService.DeleteChapter(id);
            if (isDeleted == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}