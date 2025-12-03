

using System.Text.Json.Serialization;

namespace RentalApp.Core;

internal interface IEquipment : IEquatable<IEquipment>, IComparable<IEquipment>, IHasIdentifier
{
    string Barcode { get; init; }
    IReadOnlyList<Sport> Sports { get; init; }

    [JsonPropertyName("Equipment type")]
    EquipmentType EquipmentType { get; init; }
    string? Brand { get; init; }
    string? Description { get; init; }

}


