using BookStore.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.DTOs.Book.BookBorrowingRequest
{
	public class BorrowingRequestViewModel
	{
		public int BookBorrowingRequestId { get; set; }
		public string UserRequestName { get; set; }
		public string? UserApprovedName { get; set; }
		public RequestStatusEnum Status { get; set; }
		public DateTime RequestDate { get; set; }
	}
}
