using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book;
using BookStore.Data.Entities;
using System.Linq.Expressions;

namespace BookStore.Data.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<Books>
    {
	}
}
