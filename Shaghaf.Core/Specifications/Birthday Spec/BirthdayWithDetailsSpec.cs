using Shaghaf.Core.Entities.BirthdayEntity;
using Shaghaf.Core.Specifications;

public class BirthdayWithDetailsSpec : BaseSpecifications<Birthday>
{
    public BirthdayWithDetailsSpec(int id) : base(b => b.Id == id)
    {
        Includes.Add(b => b.Cakes);
        Includes.Add(b => b.Decorations);
        Includes.Add(b => b.Room); 
    }

    public BirthdayWithDetailsSpec()
    {
        Includes.Add(b => b.Cakes);
        Includes.Add(b => b.Decorations);
        Includes.Add(b => b.Room); 
    }
}
