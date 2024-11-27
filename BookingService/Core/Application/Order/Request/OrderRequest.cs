using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Order.Request
{
    public class OrderRequest
    {
        public string DeliveryOption { get; set; } // "entrega" ou "retirada"
        public int Quantity { get; set; }
        public Address Address { get; set; } // Endereço, necessário se for "entrega"
        public string PickupLocation { get; set; } // Local de retirada, necessário se for "retirada"
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime PreferredDate { get; set; }
        public TimeSpan PreferredTime { get; set; }
        public string AdditionalInstructions { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        public static implicit operator Address(Domain.Order.Entities.Address v)
        {
            throw new NotImplementedException();
        }
    }
}
