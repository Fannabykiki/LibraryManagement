namespace BookStore.Common.DTOs.Shipping
{
    public class ShippingDetailViewModel
    {
        public string? CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? ShippingDate { get; set; }
    }
}
