using System.Diagnostics;

namespace RentalApp.Core;

internal class DatabaseManager<TIRecord, TRecord> : IDatabaseManager
    where TIRecord : IHasIdentifier
    where TRecord : class, TIRecord
{

    private readonly string _databaseFile;
    private readonly IJsonParseManager _jsonParseManager;
    private IEnumerable<TRecord> _records = default!;

    public DatabaseManager(string databaseFile, IJsonParseManager jsonParseManager)
    {
        if (string.IsNullOrEmpty(databaseFile))
        {
            throw new ArgumentNullException("Please provide a valid database file path.");
        }
        _databaseFile = databaseFile;
        _jsonParseManager = jsonParseManager;
        LoadRecords();
    }

    public string AddRecord(string jsonString)
    {
        Result<TRecord, string> result = _jsonParseManager.ParseRecord<TRecord>(jsonString);
        string toReturn = default!;
        result.Visit((record) => toReturn = AddRecord(record),
                    (error) => toReturn = error);
        return toReturn;
    }

    private string AddRecord(TRecord record)
    {
        if (AddRecordBool(record) is false)
        {
            return "Record is already in the database.";
        }
        SaveRecords();
        return "Record added successfully.";
    }

    private bool AddRecordBool(TRecord record)
    {
        if (RecordExists(record.Identifier))
        {
            return false;
        }
        _records = _records.Concat(new List<TRecord> { record });
        return true;
    }

    private bool RecordExists(string identifier)
    {
        return _records.Any(record => record.Identifier == identifier);
    }

    public string DeleteRecord(string identifier)
    {
        if (DeleteRecordBool(identifier) is false)
        {
            return "Record could not be found in the database.";
        }
        SaveRecords();
        return "Record deleted successfully.";
    }

    private bool DeleteRecordBool(string identifier)
    {
        if (RecordExists(identifier) is false)
        {
            return false;
        }
        _records = _records.Where(record => record.Identifier != identifier);
        return true;
    }

    public string UpdateRecord(string jsonString)
    {
        Result<TRecord, string> result = _jsonParseManager.ParseRecord<TRecord>(jsonString);
        string toReturn = default!;
        result.Visit((record) => toReturn = UpdateRecord(record),
                    (error) => toReturn = error);
        return toReturn;
    }

    private string UpdateRecord(TRecord record)
    {
        if (DeleteRecordBool(record.Identifier) is false)
        {
            return "Record could not be found in the database.";
        }
        if (AddRecordBool(record) is false)
        {
            throw new UnreachableException("Database is in an invalid state.");
        }
        SaveRecords();
        return "Record updated successfully.";
    }

    private void LoadRecords()
    {
        _records = _jsonParseManager.LoadRecords<TRecord>(_databaseFile);
    }

    private void SaveRecords()
    {
        _jsonParseManager.SaveRecords<TRecord>(_databaseFile, _records);
    }

}
