using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Data.Repositories.Implements
{
    public class BookRepository : BaseRepository<Books>, IBookRepository
    {
        public BookRepository(BookStoreContext context) : base(context)
        {
        }
	}
}