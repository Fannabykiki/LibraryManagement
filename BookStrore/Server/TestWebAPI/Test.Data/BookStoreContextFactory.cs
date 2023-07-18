using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookStore.Data
{
	public class BookStoreContextFactory : IDesignTimeDbContextFactory<BookStoreContext>
	{
		public BookStoreContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<BookStoreContext>();
			optionsBuilder.UseSqlServer("Server=DESKTOP-OF0V18H\\FANNABY;Database=BookStore;Trusted_Connection=True;MultipleActiveResultSets=true");

			return new BookStoreContext(optionsBuilder.Options);
		}
	}
}
