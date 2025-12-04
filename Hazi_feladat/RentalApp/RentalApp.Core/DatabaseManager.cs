using System.Diagnostics;

namespace RentalApp.Core;

internal class DatabaseManager<TIRecord, TRecord> : IDatabaseManager<TIRecord>
    where TIRecord : IHasIdentifier
    where TRecord : class, TIRecord
{

    private readonly string _databaseFile;
    private readonly IJsonParseManager _jsonParseManager;
    private IEnumerable<TIRecord> _records = default!;

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

    public bool RecordExists(string identifier)
    {
        return _records.Any(record => record.Identifier == identifier);
    }

    public void AddRecord(TIRecord record)
    {
        _records = _records.Concat(new List<TIRecord> { record });
        SaveRecords();
    }

    public void DeleteRecord(string identifier)
    {
        DeleteRecordWoutSaving(identifier);
        SaveRecords();
    }

    private void DeleteRecordWoutSaving(string identifier)
    {
        _records = _records.Where(record => record.Identifier != identifier);   
    }

    public void UpdateRecord(TIRecord record)
    {
        DeleteRecordWoutSaving(record.Identifier);
        AddRecord(record);
    }

    private void LoadRecords()
    {
        _records = _jsonParseManager.LoadRecords<TIRecord,TRecord>(_databaseFile);
    }

    private void SaveRecords()
    {
        _jsonParseManager.SaveRecords<TIRecord>(_databaseFile, _records);
    }

}
