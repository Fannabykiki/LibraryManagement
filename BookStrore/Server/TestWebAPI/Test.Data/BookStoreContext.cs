using Microsoft.EntityFrameworkCore;

using BookStore.Data.Entities;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategoryDetail>().HasKey(sc => new { sc.BookId, sc.CategoryId });

            modelBuilder.Entity<BookCategoryDetail>()
                        .HasOne(sc => sc.Book)
                        .WithMany(s => s.BookCategoryDetails)
                        .HasForeignKey(sc => sc.BookId);


            modelBuilder.Entity<BookCategoryDetail>()
                        .HasOne<Category>(sc => sc.Category)
                        .WithMany(s => s.BookCategoryDetails)
                        .HasForeignKey(sc => sc.CategoryId);

            modelBuilder.Entity<BookBorrowingRequestDetails>().HasKey(sc => new { sc.BookId, sc.BookBorrowingRequestId });

            modelBuilder.Entity<BookBorrowingRequestDetails>()
                        .HasOne(sc => sc.Book)
                        .WithMany(s => s.BookBorrowingRequestDetails)
                        .HasForeignKey(sc => sc.BookId);


            modelBuilder.Entity<BookBorrowingRequestDetails>()
                        .HasOne(sc => sc.BookBorrowingRequest)
                        .WithMany(s => s.BookBorrowingRequestDetails)
                        .HasForeignKey(sc => sc.BookBorrowingRequestId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<BookBorrowingRequest> BookBorrowingRequests { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookBorrowingRequestDetails> BookBorrowingRequestDetails { get; set; }
        public DbSet<BookCategoryDetail> BookCategoryDetails { get; set; }
    }
}
