using System;

namespace Domain.Order.Entities
{

    public class Order
    {
        public int Id { get; set; }
        public string DeliveryOption { get; set; } // "entrega" ou "retirada"
        public int? AddressId { get; set; } // FK para Address
        public Address? Address { get; set; } // Endereço, necessário se for "entrega"
        public int UserId { get; set; } // FK para User
        public User User { get; set; } // Usuário que fez o pedido
        public string AdditionalInstructions { get; set; }
        public bool IsCompleted { get; private set; } // Indica se o pedido foi retirado ou entregue

        // Propriedade para o produto e a quantidade de itens
        public int ProductId { get; set; } // FK para Product
        public Product Product { get; set; } // Produto associado ao pedido
        public int ProductQuantity { get; set; } // Quantidade de itens do produto

        public bool IsValid
        {
            get
            {
                try
                {
                    ValidateState();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Marca o pedido como concluído.
        /// </summary>
        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }

        /// <summary>
        /// Método para validar o estado do pedido.
        /// </summary>
        private void ValidateState()
        {
            if (string.IsNullOrWhiteSpace(DeliveryOption) ||
                !(DeliveryOption.Equals("entrega", StringComparison.OrdinalIgnoreCase) ||
                  DeliveryOption.Equals("retirada", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("DeliveryOption deve ser 'entrega' ou 'retirada'.");
            }

            if (ProductQuantity <= 0)
            {
                throw new ArgumentException("ProductQuantity deve ser maior que zero.");
            }

            if (DeliveryOption.Equals("entrega", StringComparison.OrdinalIgnoreCase) && Address == null)
            {
                throw new ArgumentException("Address é obrigatório para pedidos de entrega.");
            }
        }
    }

}
