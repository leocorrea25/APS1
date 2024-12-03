using System;

namespace Domain.Order.Entities
{
    public class OrderRequest
    {
        public string DeliveryOption { get; set; } // "entrega" ou "retirada"
        public string AdditionalInstructions { get; set; }
        public int ProductId { get; set; } // FK para Product
        public int ProductQuantity { get; set; } // Quantidade de itens do produto
        public int UserId { get; set; } // FK para User
    }
}
