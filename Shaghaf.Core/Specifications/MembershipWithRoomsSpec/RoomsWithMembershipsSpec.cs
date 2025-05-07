using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Specifications;

public class RoomsWithMembershipsSpec : BaseSpecifications<Room>
{
    public RoomsWithMembershipsSpec()
    {
        AddInclude(r => r.Memberships); // Include Memberships in the query
    }

    public RoomsWithMembershipsSpec(int roomId) : base(r => r.Id == roomId)
    {
        AddInclude(r => r.Memberships); // Include Memberships in the query
    }
}
