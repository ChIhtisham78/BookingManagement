using Shaghaf.Core.Entities.OrderEntities;
using System.Linq;

namespace Shaghaf.Core.Specifications
{
    public class OrdersByPhoneSpecification : BaseSpecifications<Order>
    {
        public OrdersByPhoneSpecification(string phone) : base(o => o.Phone == phone)
        {
            AddInclude(o => o.OrderItems); // Include the order items
            AddInclude(o => o.Booking); // Optionally include the booking if linked
        }
    }

}
