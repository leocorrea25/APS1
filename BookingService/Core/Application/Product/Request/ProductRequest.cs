using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order.Requests
{
    public class ProductRequest
    {
        public int Id { get; set; } // ID do produto
        public string Name { get; set; } // Nome do produto
        public string Description { get; set; } // Descrição do produto
        public decimal Price { get; set; } // Preço do produto
        public int Quantity { get; set; } // Quantidade de itens do produto
    }
}
