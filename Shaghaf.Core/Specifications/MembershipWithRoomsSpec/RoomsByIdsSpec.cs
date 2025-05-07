using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Specifications;

public class RoomsByIdsSpec : BaseSpecifications<Room>
{
    public RoomsByIdsSpec(ICollection<int> roomIds) : base(r => roomIds.Contains(r.Id))
    {
    }
}
