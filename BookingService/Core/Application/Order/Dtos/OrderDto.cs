using System;
using Domain.Order.Entities;

namespace Application.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string DeliveryOption { get; set; }
        public int Quantity { get; set; }
        public AddressDto Address { get; set; }
        public string PickupLocation { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime PreferredDate { get; set; }
        public TimeSpan PreferredTime { get; set; }
        public string AdditionalInstructions { get; set; }
        public bool IsCompleted { get; set; }

        public static Domain.Order.Entities.Order MapToEntity(OrderDto orderDto)
        {
            return new Domain.Order.Entities.Order
            {
                Id = orderDto.Id,
                DeliveryOption = orderDto.DeliveryOption,
                Quantity = orderDto.Quantity,
                Address = orderDto.Address != null ? new Address
                {
                    Street = orderDto.Address.Street,
                    City = orderDto.Address.City,
                    PostalCode = orderDto.Address.PostalCode
                } : null,
                PickupLocation = orderDto.PickupLocation,
                ContactName = orderDto.ContactName,
                ContactPhone = orderDto.ContactPhone,
                ContactEmail = orderDto.ContactEmail,
                PreferredDate = orderDto.PreferredDate,
                PreferredTime = orderDto.PreferredTime,
                AdditionalInstructions = orderDto.AdditionalInstructions,
                
            };
        }

        public static OrderDto MapToDto(Domain.Order.Entities.Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                DeliveryOption = order.DeliveryOption,
                Quantity = order.Quantity,
                Address = order.Address != null ? new AddressDto
                {
                    Street = order.Address.Street,
                    City = order.Address.City,
                    PostalCode = order.Address.PostalCode
                } : null,
                PickupLocation = order.PickupLocation,
                ContactName = order.ContactName,
                ContactPhone = order.ContactPhone,
                ContactEmail = order.ContactEmail,
                PreferredDate = order.PreferredDate,
                PreferredTime = order.PreferredTime,
                AdditionalInstructions = order.AdditionalInstructions,
                IsCompleted = order.IsCompleted
            };
        }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
