using System.Text.Json.Serialization;

namespace SportyBuddies.Contracts.Sports;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SportType
{
    BallSport,
    CombatSport,
    Gymnastics,
    WaterSport,
    WinterSport
}