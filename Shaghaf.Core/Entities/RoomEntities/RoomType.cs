using System.Runtime.Serialization;

namespace Shaghaf.Core.Entities.RoomEntities
{
    public enum RoomType
    {
        [EnumMember(Value = "TrainingRoom")]
        TrainingRoom, // Training sessions
        [EnumMember(Value = "FunnyRoom")]
        FunnyRoom, // Entertainment
        [EnumMember(Value = "MeetingRoom")]
        MeetingRoom, // Meetings
        [EnumMember(Value = "EventRoom")]
        EventRoom, // Events
        [EnumMember(Value = "BirthdayRoom")]
        BirthdayRoom, // Birthday parties
        [EnumMember(Value = "WorkSpace")]
        WorkSpace, // Shared workspace
        [EnumMember(Value = "GameRoom")]
        GameRoom, // Gaming
        [EnumMember(Value = "ConferenceRoom")]
        ConferenceRoom // Conferences
    }
}