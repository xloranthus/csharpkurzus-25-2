
using System.Text.Json;
using System.Diagnostics;

namespace RentalApp.Core;

public class RentalApp : IRentalApp
{

    private readonly string _databaseFile;
    private IEnumerable<IEquipment> _equipments;

    public RentalApp(string databaseFile)
    {
        if(string.IsNullOrEmpty(databaseFile))
        {
            throw new ArgumentNullException("Please provide a valid database file path.");
        }
        _databaseFile = databaseFile;
        LoadEquipmentsFromDatabase();
    }

    public string AddEquipment(string JSONString)
    {
        Result<IEquipment, string> result = ParseEquipment(JSONString);
        string toReturn = default!;
        result.Visit((equipment) => toReturn = AddEquipment(equipment),
                    (error) => toReturn = error);
        return toReturn;
    }

    private string AddEquipment(IEquipment equipment)
    {
        if (AddEquipmentBool(equipment) is false)
        {
            return "Equipment is already in the database.";
        }
        SaveEquipmentsToDatabase();
        return "Equipment added successfully.";
    }

    private bool AddEquipmentBool(IEquipment equipment)
    {
        if (EquipmentExists(equipment.Barcode))
        {
            return false;
        }
        _equipments = _equipments.Concat(new List<IEquipment> { equipment });
        return true;
    }

    private bool EquipmentExists(string Barcode)
    {
        return _equipments.Any(equipment => equipment.Barcode == Barcode);
    }

    public string DeleteEquipment(string Barcode)
    {
        if (DeleteEquipmentBool(Barcode) is false)
        {
            return "Equipment could not be found in the database.";
        }
        SaveEquipmentsToDatabase();
        return "Equipment deleted successfully.";
    }

    private bool DeleteEquipmentBool(string Barcode)
    {
        if (EquipmentExists(Barcode) is false)
        {
            return false;
        }
        _equipments = _equipments.Where(equipment => equipment.Barcode != Barcode);
        return true;
    }

    public string UpdateEquipment(string JSONString)
    {
        Result<IEquipment, string> result = ParseEquipment(JSONString);
        string toReturn = default!;
        result.Visit((equipment) => toReturn = UpdateEquipment(equipment),
                    (error) => toReturn = error);
        return toReturn;
    }

    private string UpdateEquipment(IEquipment equipment)
    {
        if (DeleteEquipmentBool(equipment.Barcode) is false)
        {
            return "Equipment could not be found in the database.";
        }
        if(AddEquipmentBool(equipment) is false)
        {
            throw new UnreachableException("Database is in an invalid state.");
        }
        SaveEquipmentsToDatabase();
        return "Equipment updated successfully.";
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

    private void LoadEquipmentsFromDatabase()
    {
        string JSONString;
        if (File.Exists(_databaseFile) && string.IsNullOrEmpty(JSONString = File.ReadAllText(_databaseFile)) is false)
        {
            _equipments = JsonSerializer.Deserialize<List<Equipment>>(JSONString)!;
        }
        else
        {
            _equipments = new List<IEquipment>();
        }
    }

    private void SaveEquipmentsToDatabase()
    {
        string JSONString = JsonSerializer.Serialize<IEnumerable<IEquipment>>(_equipments, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_databaseFile, JSONString);
    }
    
    
}
