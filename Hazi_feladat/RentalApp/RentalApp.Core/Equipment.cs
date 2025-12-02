

namespace RentalApp.Core;

internal class Equipment : IEquipment
{
    public required string Barcode { get; init; }
    
    public required IReadOnlyList<string> Sports { get; init; }
    
    public required string EquipmentType { get; init; } = "EqType";
    
    public required string Description { get; init; } = "EqDesc";

    public int CompareTo(IEquipment? other)
    {
        if (other is null)
        {
            return 1;
        }
        return Barcode.CompareTo(other.Barcode);
    }

    public bool Equals(IEquipment? other)
    {
        return other is not null && Barcode == other.Barcode;
    }

    public override bool Equals(object? obj)
    {
        return obj is not null && obj is IEquipment equipment && Barcode == equipment.Barcode;
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
