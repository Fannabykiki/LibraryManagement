using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using FluentValidation;

namespace BookStore.API.Extensions.Validation.BookBorrowingValidator
{
	public class UpdateBookBorrowingValidator : AbstractValidator<UpdateBorrowingRequest>
	{
		public UpdateBookBorrowingValidator()
		{
			RuleFor(book => book.RequestStatus).NotNull().NotEmpty().WithMessage("BookName is required").WithMessage("BookName length is between 1 -50 char");
			// Validate PublishedDate with a custom error message
			// Validate Age for submitted book has to be between 21 and 100 years old
			RuleFor(book => book.UserApprovedName).NotNull().NotEmpty();
		}
	}
}
