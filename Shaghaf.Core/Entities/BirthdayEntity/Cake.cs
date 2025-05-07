

namespace Shaghaf.Core.Entities.BirthdayEntity
{
    public class Cake : BaseEntity
    {

        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ServingSize { get; set; } // New property for serving size as a string
        public int BirthdayId { get; set; } // Foreign key
        public Birthday Birthday { get; set; }
    }

}