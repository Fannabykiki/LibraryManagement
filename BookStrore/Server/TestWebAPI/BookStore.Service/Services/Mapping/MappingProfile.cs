using AutoMapper;
using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Data.Entities;

namespace BookStore.Service.Services.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<BookBorrowingRequest, BorrowingRequestViewModel>().ForMember(dest => dest.UserRequestName, opt => opt.MapFrom(src => src.User.UserName));

			CreateMap<BookBorrowingRequestDetails, DetailViewModel>().ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.BookName));

			CreateMap<Books, BookViewModel>().ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
		}
	}
}
