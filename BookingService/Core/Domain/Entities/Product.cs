namespace Domain.Entities
{
    public class Product
    {
        public int Id { get; set; } // ID do produto
        public string Name { get; set; } // Nome do produto
        public string Description { get; set; } // Descrição do produto
        public decimal Price { get; set; } // Preço do produto
        public int Quantity { get; set; } // Quantidade de itens do produto
        public int UserId { get; set; } // ID do usuário que criou o produto
        public User User { get; set; } // Usuário que criou o produto
    }
}
