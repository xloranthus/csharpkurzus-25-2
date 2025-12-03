namespace RentalApp.Core;

internal interface IDatabaseManager
{
    string AddRecord(string jsonString);
    string DeleteRecord(string identifier);
    string UpdateRecord(string jsonString);
}