using AutoMapper;
using BooksApi.Models;
using BooksApi.ViewModels;

namespace BooksApi.AutoMappers
{
    public class BookProfile : Profile
    {
        public BookProfile(){
            CreateMap<Book, BookViewModel>().ReverseMap();
        }
    }
}