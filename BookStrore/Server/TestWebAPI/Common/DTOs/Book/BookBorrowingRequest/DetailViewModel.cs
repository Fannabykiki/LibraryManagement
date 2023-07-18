using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.DTOs.Book.BookBorrowingRequest
{
	public class DetailViewModel
	{
        public string BookName { get; set; }
        public int BookBorrowingRequestId { get; set; }
    }
}
