namespace Application.Order.Request
{
    public class EditOrderRequest
    {
        public int OrderId { get; set; }
        public string DeliveryOption { get; set; } // "entrega" ou "retirada"
        public string AdditionalInstructions { get; set; }
        public int ProductId { get; set; } // FK para Product
        public int ProductQuantity { get; set; } // Quantidade de itens do produto
        public int UserId { get; set; } // FK para User
    }
}
