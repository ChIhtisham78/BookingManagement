using Shaghaf.Core.Entities;

namespace Shaghaf.Core.Specifications
{
    // Specification for filtering menu items by category
    public class MenuItemByCategorySpec : BaseSpecifications<MenuItem>
    {
        // Constructor that initializes the specification with a category filter
        public MenuItemByCategorySpec(string category)
            : base(m => m.Category.ToLower() == category.ToLower())
        {
            AddOrderBy(m => m.Name); // Order by Name 
        }
    }
}
