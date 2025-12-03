namespace RentalApp.Core;
    

public class RentalApp : IRentalApp
{

    private readonly IJsonParseManager _jsonParseManager;
    private readonly IDatabaseManager _databaseManager;

    public RentalApp(string databaseFile)
    {
        _jsonParseManager = new JsonParseManager();
        _databaseManager = new DatabaseManager<IEquipment, Equipment>(databaseFile, _jsonParseManager);
    }

    public string AddEquipment(string jsonString)
    {
        return _databaseManager.AddRecord(jsonString);
    }

    public string DeleteEquipment(string barcode)
    {
        return _databaseManager.DeleteRecord(barcode);
    }

    public string UpdateEquipment(string jsonString)
    {
        return _databaseManager.UpdateRecord(jsonString);
    }
}
