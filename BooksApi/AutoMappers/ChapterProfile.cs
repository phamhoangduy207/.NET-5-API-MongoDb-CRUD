using AutoMapper;
using BooksApi.Models;
using BooksApi.ViewModels;

namespace BooksApi.AutoMappers
{
    public class ChapterProfile : Profile
    {
        public ChapterProfile(){
            CreateMap<Chapter, ChapterViewModel>().ReverseMap();
        }
    }
}