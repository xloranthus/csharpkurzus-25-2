

namespace RentalApp.Core;


public interface IRentalApp
{
    public string AddEquipment(string JSONString);
    public string DeleteEquipment(string Barcode);
}
