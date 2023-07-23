using BookStore.Data.Entities;
using FluentValidation;

namespace BookStore.API.Extensions.Validation.BookBorrowingValidator
{
	public class AddBookBorrowingValidator : AbstractValidator<BookBorrowingRequest>
	{
		public AddBookBorrowingValidator()
		{
			RuleFor(book => book.Status).NotNull().NotEmpty().WithMessage("BookName is required").WithMessage("BookName length is between 1 -50 char");
			RuleFor(book => book.RequestDate).NotNull().NotEmpty().WithMessage("PublisherName is required").WithMessage("PublisherName length is between 1 -50 char");
			// Validate PublishedDate with a custom error message
			// Validate Age for submitted book has to be between 21 and 100 years old
			RuleFor(book => book.UserRquestId).NotNull().NotEmpty();
			RuleFor(book => book.UserApprovedName).NotNull().NotEmpty();
		}

	}
}
