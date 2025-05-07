using Shaghaf.Core.Entities.BirthdayEntity;
using Shaghaf.Core.Entities.RoomEntities;

namespace Shaghaf.Core.Entities.BirthdayEntity
{
    public class Birthday : BaseEntity
    {
        public string UserName { get; set; } = null!; // Name of the user
        public DateTime Date { get; set; } // Date of the birthday
        public int NumberOfGuests { get; set; } // Number of guests
        public ICollection<Cake> Cakes { get; set; } = new List<Cake>(); // List of cakes
        public ICollection<Decoration> Decorations { get; set; } = new List<Decoration>(); // List of decorations
      //  public ICollection<PhotoSession> PhotoSessions { get; set; } = new List<PhotoSession>(); // List of photo sessions
        public int RoomId { get; set; } // ID of the associated room
        public Room Room { get; set; } = null!; // Associated room
    }
}
