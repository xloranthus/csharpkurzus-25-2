

namespace RentalApp.Core;

internal class Equipment : IEquipment, IEquatable<Equipment>, IComparable<Equipment>
{
    public required string Barcode { get; init; }
    
    public required IReadOnlyList<string> Sports { get; init; }
    
    public required string EquipmentType { get; init; } = "EqType";
    
    public required string Description { get; init; } = "EqDesc";

    public int CompareTo(Equipment? other)
    {
        if(other is null)
        {
            return 1;   
        }
        return Barcode.CompareTo(other.Barcode);
    }

    public bool Equals(Equipment? other)
    {
        return other is not null && Barcode == other.Barcode;
    }

    public override int GetHashCode()
    {
        return Barcode.GetHashCode();
    }

    public override string ToString()
    {
        return $"Barcode: {Barcode}, Sports: {string.Join(", ", Sports)}, EquipmentType: {EquipmentType}, Description: {Description}";
    }
}
