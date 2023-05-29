using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BookStore.Common.Enums;

namespace BookStore.Data.Entities
{
    public class BookBorrowingRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookBorrowingRequestId { get; set; }
        [ForeignKey("User")]
        public Guid UserRquestId { get; set; }
        public Guid? UserApproveId { get; set; }
        public virtual User User { get; set; }
        public RequestStatusEnum Status { get; set; }
        public DateTime RequestDate { get; set; }
        public Shipping Shipping { get; set; }
        public List<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }
    }
}
