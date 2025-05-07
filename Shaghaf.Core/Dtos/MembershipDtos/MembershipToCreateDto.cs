using System.Collections.Generic;

namespace Shaghaf.Core.Dtos.MembershipDtos
{
    public class MembershipToCreateDto
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public int DurationInDays { get; set; }
        public int MaxGuests { get; set; }
        public List<int> RoomIds { get; set; } = new List<int>(); // Changed from ICollection<int> to List<int>
    }
}
