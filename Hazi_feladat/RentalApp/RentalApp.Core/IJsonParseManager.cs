
namespace RentalApp.Core
{
    internal interface IJsonParseManager
    {
        IEnumerable<TRecord> LoadRecords<TRecord>(string databaseFile);
        Result<TRecord, string> ParseRecord<TRecord>(string jsonString);
        void SaveRecords<TRecord>(string databaseFile, IEnumerable<TRecord> records);
    }
}