namespace RentalApp.Core;

internal interface IDatabaseManager<TIRecord>
{
    public bool RecordExists(string identifier);

    public void AddRecord(TIRecord record);

    public void DeleteRecord(string identifier);

    public void UpdateRecord(TIRecord record);
}