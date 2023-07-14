using BookStore.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Common.DTOs.Shipping
{
	public class CreateShippingRequest
	{
		public string ReceiverName { get; set; }
		public int BookBorrowingRequestId { get; set; }
		public DateTime CreateDate { get; set; }
		public string? CompanyName { get; set; }
		public string Address { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime? ShippingDate { get; set; }
	}
}
