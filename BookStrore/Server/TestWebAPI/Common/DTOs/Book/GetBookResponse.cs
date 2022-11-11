using BookStore.API.DTOs;
using BookStore.Common.DTOs.Base;

namespace BookStore.Common.DTOs.Book
{
    
    public class GetBooksResponse : BaseResponse
    {
        public List<BookViewModel> BookViewModels { get; set; }
    }
}