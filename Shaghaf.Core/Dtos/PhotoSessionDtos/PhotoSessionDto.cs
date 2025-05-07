using System;

namespace Shaghaf.Core.Dtos.PhotoSessionDtos
{
    public class PhotoSessionDto
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public int? RoomId { get; set; }
    }
}
