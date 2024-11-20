using Domain.Enums;
using Domain.Room.Enums;
using Domain.Room.ValueObjects;
using Domain.ValueObjects;
namespace Application.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
        public bool IsAvailable { get; set; }
        public bool HasGuest { get; set; }

        public static Domain.Entities.Room MapToEntity(RoomDto roomDto)
        {
            return new Domain.Entities.Room
            {
                Id = roomDto.Id,
                Name = roomDto.Name,
                Level = roomDto.Level,
                InMaintenance = roomDto.InMaintenance,
                Price = new Price
                {
                    Value = roomDto.PriceAmount,
                    Currency = roomDto.PriceCurrency switch
                    {
                        "USD" => AcceptedCurrencies.Dolar,
                        "EUR" => AcceptedCurrencies.Euro,
                        "BTC" => AcceptedCurrencies.Bitcoin,
                        _ => throw new ArgumentException("Moeda não suportada")
                    }
                }
            };
        }

        public static RoomDto MapToDto(Domain.Entities.Room room)
        {
            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Level = room.Level,
                InMaintenance = room.InMaintenance,
                PriceAmount = room.Price.Value,
                PriceCurrency = room.Price.Currency switch
                {
                    AcceptedCurrencies.Dolar => "USD",
                    AcceptedCurrencies.Euro => "EUR",
                    AcceptedCurrencies.Bitcoin => "BTC",
                    _ => throw new ArgumentException("Moeda não suportada")
                },
                IsAvailable = room.IsAvailable,
                HasGuest = room.HasGuest
            };
        }
    }
}
