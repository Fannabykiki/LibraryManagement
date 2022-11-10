using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Entities
{
    public class BookCategoryDetail
    {
        public int BookId { get; set; }
        public Books Book { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
