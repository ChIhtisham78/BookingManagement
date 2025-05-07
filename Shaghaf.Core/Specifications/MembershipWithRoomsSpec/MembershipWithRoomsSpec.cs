using Shaghaf.Core.Entities.MembershipEntity;
using Shaghaf.Core.Specifications;

public class MembershipWithRoomsSpec : BaseSpecifications<Membership>
{
    public MembershipWithRoomsSpec(int id) : base(m => m.Id == id)
    {
        Includes.Add(m => m.Rooms);
    }

    public MembershipWithRoomsSpec()
    {
        Includes.Add(m => m.Rooms);
    }
}
