using BookStore.API.DTOs;
using BookStore.Common.DTOs.Book.BookBorrowingRequest;
using BookStore.Common.DTOs.Book.BorrowingRequestDetail;
using BookStore.Common.DTOs.Book;
using BookStore.Data.Entities;
using BookStore.Common.DTOs.ShippingDTO;
using BookStore.Common.DTOs.ShippingDTOs;
using BookStore.Common.DTOs.Shipping;

namespace BookStore.Service.Services.ShippingService
{
    public interface IShippingService
    {
        Task<IEnumerable<Shipping>> GetAllShippingAsync();
        Task<ShippingDetailViewModel> GetShippingByShippingIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<UpdateShippingResponse> UpdateShippingStatus(UpdateShippingStatus updateShippingStatus,int id);
        Task<UpdateShippingResponse> UpdateShippingDetailAsync(UpdateShippingDetailRequest updateShippingDetailRequest,int id);
		Task<CreateShippingResponse> CreateAsync(CreateShippingRequest createShippingRequest);
	}
}
