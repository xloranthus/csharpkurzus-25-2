
namespace RentalApp.Core;

internal interface IEquipment
{
    
    string Barcode { get; init; }

    IReadOnlyList<string> Sports {  get; init; }

    string EquipmentType { get; init; }

    string Description {  get; init; }

}


