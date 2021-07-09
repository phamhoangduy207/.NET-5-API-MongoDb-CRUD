namespace BooksApi.ViewModels
{
    public class ChapterViewModel
    {
        public string Id { get; set; }
        public string BookId { get; set; }
        public string ChapterName { get; set; }
        public int Page { get; set; }
    }
}