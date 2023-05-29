using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookStore.Common.Enums;

namespace BookStore.Data.Entities
{
    public class Shipping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShippingId { get; set; }
        public string ReceiverName { get; set; }
        [ForeignKey("BookBorrowingRequest")]
        public int BookBorrowingRequestId { get; set; }
        public DateTime CreateDate { get; set; }
        public ShippingStatus Status { get; set;}
        public ShippingDetail ShippingDetail { get; set; }
    }
}
