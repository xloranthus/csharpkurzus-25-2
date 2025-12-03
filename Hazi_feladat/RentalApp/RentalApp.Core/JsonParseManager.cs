
using System.Text.Json;

namespace RentalApp.Core;

internal class JsonParseManager() : IJsonParseManager
{
    public Result<TRecord, string> ParseRecord<TRecord>(string jsonString)
    {
        if (string.IsNullOrEmpty(jsonString) || jsonString == "null")
        {
            return new Result<TRecord, string>("Cannot parse null or empty string.");
        }

        try
        {
            // JsonSerializer.Deserialize csak abban az esetben ad vissza null-t, ha maga a jsonString null
            TRecord record = JsonSerializer.Deserialize<TRecord>(jsonString)!;
            Console.WriteLine($"Record parsed successfully:{Environment.NewLine}{record}");
            return new Result<TRecord, string>(record);
        }
        catch (Exception ex)
        {
            if (ex is JsonException || ex is ArgumentNullException)
            {
                return new Result<TRecord, string>(ex.Message);
            }
            throw;
        }

    }

    public void SaveRecords<TRecord>(string databaseFile, IEnumerable<TRecord> records)
    {
        string jsonString = JsonSerializer.Serialize<IEnumerable<TRecord>>(records, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(databaseFile, jsonString);
    }

    public IEnumerable<TRecord> LoadRecords<TRecord>(string databaseFile)
    {
        string jsonString;
        if (File.Exists(databaseFile) is false || string.IsNullOrEmpty(jsonString = File.ReadAllText(databaseFile)))
        {
            return new List<TRecord>();
        }
        return JsonSerializer.Deserialize<List<TRecord>>(jsonString)!;
    }
}
