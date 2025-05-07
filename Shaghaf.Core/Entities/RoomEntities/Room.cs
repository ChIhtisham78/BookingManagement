using Shaghaf.Core.Entities.BirthdayEntity;
using Shaghaf.Core.Entities.MembershipEntity;

namespace Shaghaf.Core.Entities.RoomEntities
{

    public class Room : BaseEntity
    {
        public string Name { get; set; } = null!; // Room name
        public decimal Rate { get; set; } // Room rate
        public int Seat { get; set; } // Number of seats
        public string Description { get; set; } = null!; // Room description
        public string Location { get; set; } = null!; // Room location
        public DateTime Date { get; set; } // Available date
        public decimal Price { get; set; } // Room price
        public RoomPlan Plan { get; set; } = RoomPlan.Hour; // Default per hour
        public RoomType Type { get; set; } = RoomType.FunnyRoom; // Default funny
        public ICollection<PhotoSession> PhotoSessions { get; set; } = new List<PhotoSession>(); // Photo sessions
        public ICollection<Birthday> Birthdays { get; set; } = new List<Birthday>(); // Birthdays
                                                                                                        
        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();  // Navigation property for the many-to-many relationship with Membership
    }
}
