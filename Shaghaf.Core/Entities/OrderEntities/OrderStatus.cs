using System.Runtime.Serialization;

namespace Shaghaf.Core.Entities.OrderEntities
{

    public enum OrderStatus
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
