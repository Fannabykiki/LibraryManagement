namespace BookStore.Common.DTOs.ShippingDTO
{
    public class UpdateShippingDetailRequest
    {
        public string? CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ShippingDate { get; set; }
    }
}
