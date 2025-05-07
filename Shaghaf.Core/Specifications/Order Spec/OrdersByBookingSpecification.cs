using Shaghaf.Core.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Specifications.Order_Spec
{
    public class OrdersByBookingSpecification : BaseSpecifications<Order>
    {
        public OrdersByBookingSpecification(int bookingId) : base(o => o.BookingId == bookingId)
        {
            AddInclude(o => o.OrderItems); // Include the order items
        }
    }
}
