
using System.Text.Json;
using System.Linq;

namespace RentalApp.Core;

public class RentalApp : IRentalApp
{

    private readonly string _databaseFile;

    public RentalApp(string databaseFile)
    {
        if(string.IsNullOrEmpty(databaseFile))
        {
            throw new ArgumentNullException("Please provide a valid database file path.");
        }
        _databaseFile = databaseFile;
    }

    private IEnumerable<IEquipment> LoadFromDatabase()
    {
        string JSONString;
        if (File.Exists(_databaseFile) && string.IsNullOrEmpty(JSONString = File.ReadAllText(_databaseFile)) is false)
        {
            return JsonSerializer.Deserialize<List<Equipment>>(JSONString)!;
        }
        return new List<IEquipment>();
    }

    private string SaveToDatabase(IEquipment equipment)
    {
        IEnumerable<IEquipment> equipments = LoadFromDatabase();
        if (equipments.Contains(equipment))
        {
            return "Equipment is already in the database.";
        }
        equipments = equipments.Concat(new List<IEquipment>{ equipment });
        string JSONString = JsonSerializer.Serialize<IEnumerable<IEquipment>>(equipments, new JsonSerializerOptions { WriteIndented = true});
        File.WriteAllText(_databaseFile, JSONString);
        return "Equipment successfully added.";
    }

    public string AddEquipment(string JSONString)
    {
        Result<IEquipment,string> result = ParseEquipment(JSONString);
        string toReturn = default!;
        result.Visit((equipment) => toReturn = SaveToDatabase(equipment),
                    (error) => toReturn = error);
        return toReturn;
    }

    private Result<IEquipment,string> ParseEquipment(string JSONString)
    {

        if (string.IsNullOrEmpty(JSONString) || JSONString == "null")
        {
            return new Result<IEquipment, string>("Cannot add null or empty string.");
        }

        try
        {
            // JsonSerializer.Deserialize csak abban az esetben ad vissza null-t, ha maga a JSONString null
            IEquipment equipment = JsonSerializer.Deserialize<Equipment>(JSONString)!;
            Console.WriteLine(equipment);
            Console.WriteLine(equipment.Barcode);
            return new Result<IEquipment, string>(equipment);
        }
        catch (Exception ex)
        {
            return new Result<IEquipment, string>(ex.Message);
        }

    }
    
}
