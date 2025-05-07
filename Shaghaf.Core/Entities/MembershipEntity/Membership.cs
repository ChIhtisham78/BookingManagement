using Shaghaf.Core.Entities.RoomEntities;

namespace Shaghaf.Core.Entities.MembershipEntity
{
    // The Membership class is used to represent different types of memberships available for users in the system
    public class Membership : BaseEntity
    {
        public string Name { get; set; } = null!; // Name of the membership type
        public decimal Price { get; set; } // Cost of the membership
        public string Description { get; set; } = null!; // Description of what the membership includes
        public int DurationInDays { get; set; } // Duration of the membership in days
        public int MaxGuests { get; set; } // Maximum number of guests allowed

        // Navigation property for the many-to-many relationship with Room
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
