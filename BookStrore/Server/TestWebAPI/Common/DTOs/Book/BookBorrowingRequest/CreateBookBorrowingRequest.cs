﻿using BookStore.Common.DTOs.Base;
using BookStore.Common.Enums;
using System.ComponentModel.DataAnnotations;


namespace BookStore.Common.DTOs.Book.BookBorrowingRequest
{
    public class CreateBookBorrowingRequest : BaseResponse
    {
        public Guid UserRequestId { get; set; }
        public List<int> BookIds { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
