
namespace RentalApp.Core
{
    internal interface IJsonParseManager
    {

        Result<TIRecord, string> ParseRecord<TIRecord, TRecord>(string jsonString) where TRecord : class, TIRecord;
        void SaveRecords<TIRecord>(string databaseFile, IEnumerable<TIRecord> records);
        IEnumerable<TIRecord> LoadRecords<TIRecord,TRecord>(string databaseFile) where TRecord : class, TIRecord;
        
    }
}