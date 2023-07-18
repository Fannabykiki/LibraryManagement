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
        public string PublisherName { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }
    }
}