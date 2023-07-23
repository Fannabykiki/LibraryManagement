using BookStore.API.DTOs;
using BookStore.Data.Entities;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Extensions.Validation.BookValidator
{
	public  class UpdateBookValidator : AbstractValidator<UpdateBookRequest>
	{
		public UpdateBookValidator() {
			RuleFor(book => book.BookName).NotNull().NotEmpty().WithMessage("BookName is required").Length(1, 50).WithMessage("BookName length is between 1 -50 char");
			RuleFor(book => book.PublisherName).NotNull().NotEmpty().WithMessage("PublisherName is required").Length(1, 50).WithMessage("PublisherName length is between 1 -50 char");
			// Validate PublishedDate with a custom error message
			RuleFor(book => book.PublishedDate).NotEmpty().WithMessage("PublishedDate is required");
			// Validate Age for submitted book has to be between 21 and 100 years old
			RuleFor(book => book.CategoryId).NotNull().NotEmpty();
		}	
	}
}
