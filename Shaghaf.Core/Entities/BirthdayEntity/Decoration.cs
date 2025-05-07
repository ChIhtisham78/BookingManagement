
namespace Shaghaf.Core.Entities.BirthdayEntity
{
    public class Decoration : BaseEntity
    {

        public string Description { get; set; }  // e.g., "2 helium balloons, a happy birthday ribbon, and two balloons"
        public decimal Price { get; set; }  // e.g., 450 LE
        public int BirthdayId { get; set; }  // Foreign key to the Birthday event
        public Birthday Birthday { get; set; }
    }
}