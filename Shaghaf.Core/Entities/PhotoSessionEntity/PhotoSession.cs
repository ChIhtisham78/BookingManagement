using Shaghaf.Core.Entities.RoomEntities;

namespace Shaghaf.Core.Entities.BirthdayEntity
{
    public class PhotoSession : BaseEntity
    {
        public decimal Cost { get; set; } // Cost of the photo session
        public TimeSpan Duration { get; set; } // Duration of the photo session
        public DateTime Date { get; set; } // Date of the photo session
        public string Location { get; set; } = null!; // Location of the session (indoor or outdoor)
        public int? RoomId { get; set; } // ID of the associated Room (if indoor)
        public Room? Room { get; set; } // Associated Room (if indoor session)
    
    }
}
