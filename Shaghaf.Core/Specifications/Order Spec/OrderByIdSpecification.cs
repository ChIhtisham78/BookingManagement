using Shaghaf.Core.Entities.OrderEntities;
using System;
using System.Linq.Expressions;

namespace Shaghaf.Core.Specifications
{
    public class OrderByIdSpecification : BaseSpecifications<Order>
    {
        public OrderByIdSpecification(int id, string phone) : base(o => o.Id == id && o.Phone == phone)
        {
            AddInclude(o => o.OrderItems);
        }
    }
}
