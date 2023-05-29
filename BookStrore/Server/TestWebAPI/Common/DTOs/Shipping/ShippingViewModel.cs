using BookStore.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Common.DTOs.Shipping
{
    public class ShippingViewModel
    {
        public string ReceiverName { get; set; }
        public int BookBorrowingRequestId { get; set; }
        public DateTime CreateDate { get; set; }
        public ShippingStatus Status { get; set; }
    }
}

