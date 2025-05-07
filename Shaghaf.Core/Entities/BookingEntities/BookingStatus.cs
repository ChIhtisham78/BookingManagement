using System.Runtime.Serialization;

namespace Shaghaf.Core.Entities.BookingEntities
{
    public enum BookingStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,

        [EnumMember(Value = "Confirmed")]
        Confirmed,

        [EnumMember(Value = "Cancelled")]
        Cancelled,

        [EnumMember(Value = "Failed")]
        Failed
    }
}
