using Shaghaf.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity 
    {
        public Expression<Func<T, bool>>? Criteria { get; set; } // Func to send to where
        public List<Expression<Func<T, object>>> Includes { get; set; }

        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
    }
}
