
using System.Text.Json;

namespace RentalApp.Core;

internal class JsonParseManager() : IJsonParseManager
{
    public Result<TIRecord, string> ParseRecord<TIRecord,TRecord>(string jsonString)
        where TRecord : class, TIRecord
    {
        if (string.IsNullOrEmpty(jsonString) || jsonString == "null")
        {
            return new Result<TIRecord, string>("Cannot parse null or empty string.");
        }

        try
        {
            // JsonSerializer.Deserialize csak abban az esetben ad vissza null-t, ha maga a jsonString null
            TIRecord record = JsonSerializer.Deserialize<TRecord>(jsonString)!;
            Console.WriteLine($"Record parsed successfully:{Environment.NewLine}{record}");
            return new Result<TIRecord, string>(record);
        }
        catch (Exception ex)
        {
            if (ex is JsonException || ex is InvalidOperationException)
            {
                return new Result<TIRecord, string>(ex.Message);
            }
            throw;
        }

    }

    public void SaveRecords<TIRecord>(string databaseFile, IEnumerable<TIRecord> records)
    {
        string jsonString = JsonSerializer.Serialize<IEnumerable<TIRecord>>(records, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(databaseFile, jsonString);
    }

    public IEnumerable<TIRecord> LoadRecords<TIRecord,TRecord>(string databaseFile)
        where TRecord : class, TIRecord
    {
        string jsonString;
        if (File.Exists(databaseFile) is false || string.IsNullOrEmpty(jsonString = File.ReadAllText(databaseFile)))
        {
            return new List<TIRecord>();
        }
        return JsonSerializer.Deserialize<List<TRecord>>(jsonString)!;
    }
}
