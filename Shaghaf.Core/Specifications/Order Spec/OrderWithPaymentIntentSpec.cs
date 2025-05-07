using Shaghaf.Core.Entities.OrderEntities;
using Shaghaf.Core.Specifications;

public class OrderWithPaymentIntentSpec : BaseSpecifications<Order>
{
    public OrderWithPaymentIntentSpec(string paymentIntentId)
        : base(o => o.PaymentIntentId == paymentIntentId)
    {
        AddInclude(o => o.OrderItems); // Include the order items
    }
}
