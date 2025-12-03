

using System.Text.Json.Serialization;

namespace RentalApp.Core;

internal class Equipment : IEquipment
{

    private readonly string _barcode = default!;
    public required string Barcode
    {
        get
        {
            return _barcode; 
        }
        init
        {
            if(value is null)
            {
                throw new ArgumentNullException("Barcode cannot be null.");
            }
            _barcode = value;
        }
    }

    [JsonIgnore]
    public string Identifier
    {
        get
        {
            return _barcode;
        }
    }

    private readonly IReadOnlyList<Sport> _sports = default!;
    public required IReadOnlyList<Sport> Sports
    {
        get
        {
            return _sports;
        }
        init
        {
            if (value is null || value.Count == 0)
            {
                throw new ArgumentNullException("Sports cannot be null or empty list.");
            }
            _sports = value;
        }
    }

    [JsonPropertyName("Equipment type")]
    public required EquipmentType EquipmentType { get; init; }
    public string? Brand {  get; init; }
    public string? Description { get; init; }

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
        return $"Barcode: {Barcode}, Sports: {(Sports is not null ? string.Join(", ", Sports) : "")}, EquipmentType: {EquipmentType}, Description: {Description}";
    }
}
