using System.Runtime.Serialization;

namespace Shaghaf.Core.Entities.RoomEntities
{
    public enum RoomPlan
    {
        [EnumMember(Value = "Hour")]
        Hour, // Hourly bookings
        [EnumMember(Value = "Day")]
        Day, // Daily bookings
        [EnumMember(Value = "Month")]
        Month, // Monthly bookings
        [EnumMember(Value = "Week")]
        Week, // Weekly bookings
        [EnumMember(Value = "Quarter")]
        Quarter, // Quarterly bookings
        [EnumMember(Value = "Year")]
        Year // Yearly bookings
    }

}