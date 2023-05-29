using BookStore.Common.DTOs.Shipping;
using BookStore.Common.DTOs.ShippingDTO;
using BookStore.Common.DTOs.ShippingDTOs;
using BookStore.Data.Entities;
using BookStore.Data.Repositories.Interfaces;

namespace BookStore.Service.Services.ShippingService
{

    public class ShippingService : IShippingService
    {
        private readonly IShippingRepository _shippingRepository;
        private readonly IShippingDetailRepository _shippingDetailRepository;

        public ShippingService(IShippingRepository shippingRepository, IShippingDetailRepository shippingDetailRepository)
        {
            _shippingRepository = shippingRepository;
            _shippingDetailRepository = shippingDetailRepository;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var transaction = _shippingRepository.DatabaseTransaction())
                try
                {
                    var product = await _shippingRepository.GetAsync(s => s.ShippingId == id);
                    if (product == null)
                    {
                        return false;
                    }

                    _shippingRepository.DeleteAsync(product);

                    _shippingRepository.SaveChanges();

                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return false;
                }
        }

        public async Task<IEnumerable<Shipping>> GetAllShippingAsync()
        {
            return await _shippingRepository.GetAllWithOdata(x => true);
        }

        public async Task<ShippingDetailViewModel> GetShippingByShippingIdAsync(int id)
        {
            using (var transaction = _shippingRepository.DatabaseTransaction())
                try
                {
                    var result = await _shippingDetailRepository.GetAsync(c => c.ShippingId == id);

                    if (result == null)
                    {
                        return null;
                    }

                    return new ShippingDetailViewModel
                    {
                        Address = result.Address,
                        CompanyName = result.CompanyName,
                        PhoneNumber = result.PhoneNumber,
                        ShippingDate = result.ShippingDate,
                    };
                }
                catch
                {
                    transaction.RollBack();

                    return null;
                }
        }

        public async Task<UpdateShippingResponse> UpdateShippingDetailAsync(UpdateShippingDetailRequest updateShippingDetailRequest, int id)
        {
            using (var transaction = _shippingDetailRepository.DatabaseTransaction())
            {
                try
                {
                    var updateRequest = await _shippingDetailRepository.GetAsync(s => s.ShippingId == id);
                    if (updateRequest == null)
                    {
                        return new UpdateShippingResponse
                        {
                            IsSucced = false,
                        };
                    }

                    updateRequest.ShippingDate = updateRequest.ShippingDate;
                    updateRequest.PhoneNumber = updateRequest.PhoneNumber;
                    updateRequest.CompanyName = updateRequest.CompanyName;
                    updateRequest.Address = updateRequest.Address;

                    await _shippingDetailRepository.UpdateAsync(updateRequest);
                    _shippingDetailRepository.SaveChanges();

                    transaction.Commit();

                    return new UpdateShippingResponse
                    {
                        IsSucced = false,
                    };
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return new UpdateShippingResponse
                    {
                        IsSucced = false,
                    };
                }
            }
        }

        public async Task<UpdateShippingResponse> UpdateShippingStatus(UpdateShippingStatus updateShippingStatus, int id)
        {
            using (var transaction = _shippingDetailRepository.DatabaseTransaction())
            {
                try
                {
                    var updateRequest = await _shippingRepository.GetAsync(s => s.ShippingId == id);
                    if (updateRequest == null)
                    {
                        return new UpdateShippingResponse
                        {
                            IsSucced = false,
                        };
                    }

                    updateRequest.Status = updateShippingStatus.RequestStatus;

                    await _shippingRepository.UpdateAsync(updateRequest);
                    _shippingRepository.SaveChanges();

                    transaction.Commit();

                    return new UpdateShippingResponse
                    {
                        IsSucced = false,
                    };
                }
                catch (Exception)
                {
                    transaction.RollBack();

                    return new UpdateShippingResponse
                    {
                        IsSucced = false,
                    };
                }
            }
        }

        public Task<UpdateShippingResponse> UpdateShippingStatus(UpdateShippingStatus updateShippingStatus)
        {
            throw new NotImplementedException();
        }
    }
}
