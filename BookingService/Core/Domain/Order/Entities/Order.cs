using System;

namespace Domain.Order.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string DeliveryOption { get; set; } // "entrega" ou "retirada"
        public int Quantity { get; set; }
        //public int UserId { get; set; }
        public Address Address { get; set; } // Endereço, necessário se for "entrega"
        public string PickupLocation { get; set; } // Local de retirada, necessário se for "retirada"
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public DateTime PreferredDate { get; set; }
        public TimeSpan PreferredTime { get; set; }
        public string AdditionalInstructions { get; set; }
        public bool IsCompleted { get; private set; } // Indica se o pedido foi retirado ou entregue

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

            if (Quantity <= 0)
            {
                throw new ArgumentException("Quantity deve ser maior que zero.");
            }

            if (string.IsNullOrWhiteSpace(ContactName) ||
                string.IsNullOrWhiteSpace(ContactPhone) ||
                string.IsNullOrWhiteSpace(ContactEmail))
            {
                throw new ArgumentException("Informações de contato são obrigatórias.");
            }

            if (!Utils.ValidateEmail(ContactEmail))
            {
                throw new ArgumentException("Email inválido.");
            }

            if (DeliveryOption.Equals("entrega", StringComparison.OrdinalIgnoreCase) && Address == null)
            {
                throw new ArgumentException("Address é obrigatório para pedidos de entrega.");
            }

            if (DeliveryOption.Equals("retirada", StringComparison.OrdinalIgnoreCase) && string.IsNullOrWhiteSpace(PickupLocation))
            {
                throw new ArgumentException("PickupLocation é obrigatório para pedidos de retirada.");
            }
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }

    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
