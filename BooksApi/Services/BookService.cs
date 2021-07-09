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
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        private readonly ILogger<BookService> logger;

        public BookService(IBookstoreDbSettings settings, ILogger<BookService> logger)
        {
            this.logger = logger;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Book>("Books");
        }

        /* public Book Create(Book book)
        {
            book.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            _books.InsertOne(book);
            return book;
        } */
        public async Task<List<Book>> GetAllBooks()
        {
            List<Book> allBooks = null;
            try
            {
                var result = _books.Find(x => true).Sort("{BookName: 1}");
                allBooks = result.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Get all books failed.");
            }
            return allBooks;
        }
        /*         public List<Book> Get() =>
                    _books.Find(book => true).ToList(); */
        internal async Task<bool> CreateBook(Book newBookDetails)
        {
            var isCreated = false;
            try
            {
                await _books.InsertOneAsync(newBookDetails);
                isCreated = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Create book record failed.");
            }
            return isCreated;
        }
        /*         public Book Get(string id) =>
                    _books.Find(book => book.Id == id).SingleOrDefault(); */
        internal async Task<Book> GetBookById(string id)
        {
            Book foundBook = null;
            try
            {
                var result = await _books.FindAsync(book => ObjectId.Parse(id).Equals(book.Id));
                foundBook = result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Get Employee by ID failed.");
            }
            return foundBook;
        }
        /* public void Update(Book book) =>
            _books.ReplaceOne(item => item.Id == book.Id, book);
 */
        internal async Task<bool> UpdateBook(string id, Book updatedBook)
        {
            var isUpdated = false;
            try
            {
                var foundBook = GetBookById(id).Result;

                foundBook.BookName = updatedBook.BookName;
                foundBook.Price = updatedBook.Price;
                foundBook.Category = updatedBook.Category;
                foundBook.Author = updatedBook.Author;
                foundBook.Published = updatedBook.Published;

                await _books.ReplaceOneAsync(book => ObjectId.Parse(id).Equals(book.Id), foundBook);
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
        internal async Task<bool> DeleteBook(string id)
        {
            var isDeleted = false;
            try
            {
                await _books.DeleteOneAsync(book => ObjectId.Parse(id).Equals(book.Id));
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