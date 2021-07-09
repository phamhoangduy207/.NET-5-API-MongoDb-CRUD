namespace BooksApi.Models
{
    public class BookstoreDbSettings : IBookstoreDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IBookstoreDbSettings 
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}