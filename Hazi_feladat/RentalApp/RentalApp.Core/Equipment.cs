

namespace RentalApp.Core;

internal class Equipment : IEquipment
{
    public required string Barcode { get; init; }
    public required IReadOnlyList<string> Sports { get; init; }
    public required string EquipmentType { get; init; }
    public required string Description { get; init; }
}
