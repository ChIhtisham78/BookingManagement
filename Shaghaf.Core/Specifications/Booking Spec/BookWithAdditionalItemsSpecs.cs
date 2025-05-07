using Shaghaf.Core.Entities.BookingEntities;

namespace Shaghaf.Core.Specifications.Booking_Spec
{
    public class BookWithAdditionalItemsSpecs : BaseSpecifications<Booking>
    {
        public BookWithAdditionalItemsSpecs() : base()
        {
            AddInclude(b => b.Orders); // Includes associated orders if any
        }

        public BookWithAdditionalItemsSpecs(int bookId) : base(B => B.Id == bookId)
        {
            AddInclude(b => b.Orders); // Includes associated orders if any
        }
    }
}
