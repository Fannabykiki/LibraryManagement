using BookStore.Common.DTOs.Base;
using BookStore.Common.Enums;
using System.ComponentModel.DataAnnotations;


namespace BookStore.Common.DTOs.Book.BookBorrowingRequest
{
    public class CreateBookBorrowingRequest
    {
        public List<int> BookIds { get; set; }
    }
}
