


using System.Text.Json;

namespace RentalApp.Core;

public class RentalApp : IRentalApp
{
    public string AddEquipment(string JSONString)
    {

        if (string.IsNullOrEmpty(JSONString))
        {
            return "Cannot add null or empty string";
        }

        try
        {
            IEquipment? equipment = JsonSerializer.Deserialize<Equipment>(JSONString);
            return "Equipment successfully added";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }
    
}
