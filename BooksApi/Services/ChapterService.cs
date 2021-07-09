using BooksApi.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApi.Services
{
    public class ChapterService
    {
        private readonly IMongoCollection<Chapter> _chapters;
        private readonly ILogger<ChapterService> logger;

        public ChapterService(IBookstoreDbSettings settings, ILogger<ChapterService> logger)
        {
            this.logger = logger;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _chapters = database.GetCollection<Chapter>("Chapters");
        }

        /* public Book Create(Book book)
        {
            book.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            _books.InsertOne(book);
            return book;
        } */
        public async Task<List<Chapter>> GetAllChapters()
        {
            List<Chapter> allChaps = null;
            try
            {
                var result = _chapters.Find(x => true);
                allChaps = result.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Get all chapters failed.");
            }
            return allChaps;
        }
        /*         public List<Book> Get() =>
                    _books.Find(book => true).ToList(); */
        internal async Task<bool> CreateChapter(Chapter newChap)
        {
            var isCreated = false;
            try
            {
                await _chapters.InsertOneAsync(newChap);
                isCreated = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Create chapter record failed.");
            }
            return isCreated;
        }
        /*         public Book Get(string id) =>
                    _books.Find(book => book.Id == id).SingleOrDefault(); */
        internal async Task<Chapter> GetChapterById(string id)
        {
            Chapter foundChap = null;
            try
            {
                var result = await _chapters.FindAsync(chap => ObjectId.Parse(id).Equals(chap.Id));
                foundChap = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Get Employee by ID failed.");
            }
            return foundChap;
        }
        /* public void Update(Book book) =>
            _books.ReplaceOne(item => item.Id == book.Id, book);
 */
        internal async Task<bool> UpdateChapter(string id, Chapter updatedChap)
        {
            var isUpdated = false;
            try
            {
                var foundChap = GetChapterById(id).Result;

                foundChap.ChapterName = updatedChap.ChapterName;
                foundChap.Page = updatedChap.Page;

                await _chapters.ReplaceOneAsync(chap => ObjectId.Parse(id).Equals(chap.Id), foundChap);
                isUpdated = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Update book failed.");
            }
            return isUpdated;
        }
        /*         public void Remove(string id) =>
                    _books.DeleteOne(book => book.Id == id); */
        internal async Task<bool> DeleteChapter(string id)
        {
            var isDeleted = false;
            try
            {
                await _chapters.DeleteOneAsync(chap => ObjectId.Parse(id).Equals(chap.Id));
/*                 await todosService.DeleteEmployeeTodos(id); */
                isDeleted = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Delete book failed.");
            }
            return isDeleted;
        }
    }
}