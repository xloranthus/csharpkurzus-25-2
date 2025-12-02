
namespace RentalApp.Core;

internal interface IEquipment : IEquatable<IEquipment>, IComparable<IEquipment>
{

    string Barcode { get; init; }

    IReadOnlyList<string> Sports { get; init; }

    string EquipmentType { get; init; }

    string Description { get; init; }

}


