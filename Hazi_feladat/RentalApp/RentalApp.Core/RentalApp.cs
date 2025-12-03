
using System.Text.Json;

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

    public string AddEquipment(string JSONString)
    {
        Result<IEquipment, string> result = ParseEquipment(JSONString);
        string toReturn = default!;
        result.Visit((equipment) => toReturn = SaveEquipmentToDatabase(equipment),
                    (error) => toReturn = error);
        return toReturn;
    }

    public string DeleteEquipment(string Barcode)
    {
        IEnumerable<IEquipment> equipments = LoadEquipmentsFromDatabase();
        IEquipment? equipmentToBeDeleted = equipments
            .Where(equipment => equipment.Barcode == Barcode)
            .FirstOrDefault();
        if (equipmentToBeDeleted is not null)
        {
            equipments = equipments
                .Where(equipment => equipment.Barcode != Barcode);
            SaveEquipmentsToDatabase(equipments);
            return "Equipment deleted successfully.";
        }
        return "Equipment could not be found in the database.";
    }

    private Result<IEquipment, string> ParseEquipment(string JSONString)
    {

        if (string.IsNullOrEmpty(JSONString) || JSONString == "null")
        {
            return new Result<IEquipment, string>("Cannot add null or empty string.");
        }

        try
        {
            // JsonSerializer.Deserialize csak abban az esetben ad vissza null-t, ha maga a JSONString null
            IEquipment equipment = JsonSerializer.Deserialize<Equipment>(JSONString)!;
            Console.WriteLine($"Equipment parsed successfully:{Environment.NewLine}{equipment}");
            return new Result<IEquipment, string>(equipment);
        }
        catch (Exception ex)
        {
            if (ex is JsonException || ex is ArgumentNullException)
            {
                return new Result<IEquipment, string>(ex.Message);
            }
            throw;
        }

    }

    private IEnumerable<IEquipment> LoadEquipmentsFromDatabase()
    {
        string JSONString;
        if (File.Exists(_databaseFile) && string.IsNullOrEmpty(JSONString = File.ReadAllText(_databaseFile)) is false)
        {
            return JsonSerializer.Deserialize<List<Equipment>>(JSONString)!;
        }
        return new List<IEquipment>();
    }

    private string SaveEquipmentToDatabase(IEquipment equipment)
    {
        IEnumerable<IEquipment> equipments = LoadEquipmentsFromDatabase();
        if (equipments.Contains(equipment))
        {
            return "Equipment is already in the database.";
        }
        equipments = equipments.Concat(new List<IEquipment>{ equipment });
        SaveEquipmentsToDatabase(equipments);
        return "Equipment added successfully.";
    }

    private void SaveEquipmentsToDatabase(IEnumerable<IEquipment> equipments)
    {
        string JSONString = JsonSerializer.Serialize<IEnumerable<IEquipment>>(equipments, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_databaseFile, JSONString);
    }
    
    
}
