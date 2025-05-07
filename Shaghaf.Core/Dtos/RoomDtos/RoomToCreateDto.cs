namespace Shaghaf.Core.Dtos.RoomDtos
{
    public class RoomToCreateDto
    {
        public decimal Rate { get; set; }
        public string Name { get; set; } = null!;
        public int Seat { get; set; }
        public string Description { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Plan { get; set; }
    }
}
