

using RentalApp.Core;
using App = RentalApp.Core.RentalApp;


// TODO Fix:
// const string databaseFile = "C:\\Users\\xloranthus\\source\\repos\\csharpkurzus-25-2\\Hazi_feladat\\RentalApp\\RentalApp\\out.json";
const string databaseFile = "C:\\Users\\wivie\\source\\repos\\csharpkurzus-25-2\\Hazi_feladat\\RentalApp\\RentalApp\\out.json";

IRentalApp app = new App(databaseFile);

string? cmd;
Console.Write("$>");

while (string.IsNullOrEmpty(cmd = Console.ReadLine()) is false)
{

    string[] cmds = cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    if(cmds.Length != 2)
    {
        continue;
    }

    string cmdType = cmds[0];
    string fileName = cmds[1];
    
    if (File.Exists(fileName) is false)
    {
        Console.WriteLine($"{fileName} does not exist.");
        Console.Write("$>");
        continue;
    }
    
    string JSONString = File.ReadAllText(fileName);
    
    switch (cmdType)
    {
        case "addeq":
            Console.WriteLine(app.AddEquipment(JSONString));
            break;
    }

    Console.Write("$>");
}

