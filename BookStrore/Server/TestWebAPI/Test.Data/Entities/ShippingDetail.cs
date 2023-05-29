using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Entities
{
    public class ShippingDetail
    {
        [ForeignKey("Shipping")]
        public int ShippingId { get; set; }
        public Shipping Shipping { get; set; }
        public string? CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? ShippingDate { get; set; }
    }
}
