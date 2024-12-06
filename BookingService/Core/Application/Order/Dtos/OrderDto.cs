using System;
using Domain.Entities;

namespace Application.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string DeliveryOption { get; set; }
        public int Quantity { get; set; }
        public string PickupLocation { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime PreferredDate { get; set; }
        public TimeSpan PreferredTime { get; set; }
        public string AdditionalInstructions { get; set; }
        public bool IsCompleted { get; set; }

        public static Domain.Entities.Order MapToEntity(OrderDto orderDto)
        {
            return new Domain.Entities.Order
            {
                Id = orderDto.Id,
                DeliveryOption = orderDto.DeliveryOption,
                AdditionalInstructions = orderDto.AdditionalInstructions,
                
            };
        }

        public static OrderDto MapToDto(Domain.Entities.Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                DeliveryOption = order.DeliveryOption,
                AdditionalInstructions = order.AdditionalInstructions,
                IsCompleted = order.IsCompleted
            };
        }
    }
}
