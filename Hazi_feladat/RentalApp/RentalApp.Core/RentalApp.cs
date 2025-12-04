using System.Diagnostics;

namespace RentalApp.Core;
    
public class RentalApp : IRentalApp
{

    private readonly IJsonParseManager _jsonParseManager;
    private readonly IDatabaseManager<IEquipment> _equipmentDBManager;
    private readonly IDatabaseManager<ICustomer> _customerDBManager;
    private readonly IDatabaseManager<IReservation> _reservationDBManager;

    private const string ERR_ALREADY_EXIST = "{0} is already in the database.";
    private const string ERR_NOT_FOUND = "{0} could not be found in the database.";
    private const string SUCCESS = "{0} {1} successfully.";

    public RentalApp(string equipmentDBFile, string customerDBFile, string reservationDBFile)
    {
        _jsonParseManager = new JsonParseManager();
        _equipmentDBManager = new DatabaseManager<IEquipment, Equipment>(equipmentDBFile, _jsonParseManager);
        _customerDBManager = new DatabaseManager<ICustomer, Customer>(customerDBFile, _jsonParseManager);
        _reservationDBManager = new DatabaseManager<IReservation, Reservation>(reservationDBFile, _jsonParseManager);
    }

    // ADD
    public string AddEquipment(string jsonString)
    {
        return ParseRecord<IEquipment,Equipment>(jsonString, AddRecord);
    }
    public string AddCustomer(string jsonString)
    {
        return ParseRecord<ICustomer,Customer>(jsonString, AddRecord);
    }
    public string AddReservation(string jsonString)
    {
        return ParseRecord<IReservation,Reservation>(jsonString, AddRecord);
    }


    private string ParseRecord<TIRecord, TRecord>(string jsonString, Func<TIRecord,string> successAction)
        where TRecord : class, TIRecord
    {
        Result<TIRecord, string> result = _jsonParseManager.ParseRecord<TIRecord, TRecord>(jsonString);
        string toReturn = default!;
        result.Visit((record) => toReturn = successAction(record),
                    (error) => toReturn = error);
        return toReturn;
    }

    private string AddRecord<TIRecord>(TIRecord record)
    {
        if (record is IEquipment equipment)
        {
            if (_equipmentDBManager.RecordExists(equipment.Identifier))
            {
                return string.Format(ERR_ALREADY_EXIST, "Equipment");
            }
            _equipmentDBManager.AddRecord(equipment);
            return string.Format(SUCCESS, "Equipment", "added");
        }

        if (record is ICustomer customer)
        {
            if (_customerDBManager.RecordExists(customer.Identifier))
            {
                return string.Format(ERR_ALREADY_EXIST, "Customer");
            }
            _customerDBManager.AddRecord(customer);
            return string.Format(SUCCESS, "Customer", "added");
        }

        if (record is IReservation reservation)
        {
            return "TODO: Add Reservation";
        }

        throw new UnreachableException("Record has an invalid type.");
    }


    // DELETE
    public string DeleteEquipment(string identifier)
    {
        if(_equipmentDBManager.RecordExists(identifier) is false)
        {
            return string.Format(ERR_NOT_FOUND, "Equipment");
        }
        _equipmentDBManager.DeleteRecord(identifier);
        return string.Format(SUCCESS, "Equipment", "deleted");
    }

    public string DeleteCustomer(string identifier)
    {
        if (_customerDBManager.RecordExists(identifier) is false)
        {
            return string.Format(ERR_NOT_FOUND, "Customer");
        }
        _customerDBManager.DeleteRecord(identifier);
        return string.Format(SUCCESS, "Customer", "deleted");
    }

    public string DeleteReservation(string identifier)
    {
        return "TODO: Delete Reservation";
    }



    //  UPDATE
    public string UpdateEquipment(string jsonString)
    {
        return ParseRecord<IEquipment, Equipment>(jsonString, UpdateRecord);
    }

    public string UpdateCustomer(string jsonString)
    {
        return ParseRecord<ICustomer, Customer>(jsonString, UpdateRecord);
    }

    public string UpdateReservation(string jsonString)
    {
        return ParseRecord<IReservation, Reservation>(jsonString, UpdateRecord);
    }

    private string UpdateRecord<TIRecord>(TIRecord record)
    {
        if (record is IEquipment equipment)
        {
            if (_equipmentDBManager.RecordExists(equipment.Identifier) is false)
            {
                return string.Format(ERR_NOT_FOUND, "Equipment");
            }
            _equipmentDBManager.UpdateRecord(equipment);
            return string.Format(SUCCESS, "Equipment", "updated");
        }

        if (record is ICustomer customer)
        {
            if (_customerDBManager.RecordExists(customer.Identifier) is false)
            {
                return string.Format(ERR_NOT_FOUND, "Customer");
            }
            _customerDBManager.UpdateRecord(customer);
            return string.Format(SUCCESS, "Customer", "updated");
        }

        if (record is IReservation reservation)
        {
            return "TODO: Update Reservation";
        }

        throw new UnreachableException("Record has an invalid type.");
    }

}
