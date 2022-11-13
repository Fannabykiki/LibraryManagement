using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Data.Entities
{
    public class Books
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public List<BookCategoryDetail> BookCategoryDetails { get; set; }
        public List<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }
    }
}