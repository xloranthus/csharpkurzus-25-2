

using RentalApp.Core;
using App = RentalApp.Core.RentalApp;


// TODO Fix:
const string databaseFile = "C:\\Users\\xloranthus\\source\\repos\\csharpkurzus-25-2\\Hazi_feladat\\RentalApp\\RentalApp\\out.json";

IRentalApp app = new App(databaseFile);

string? cmd;

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
        throw new FileNotFoundException($"{fileName} does not exist.");
    }
    
    string JSONString = File.ReadAllText(fileName);
    
    switch (cmdType)
    {
        case "addeq":
            Console.WriteLine(app.AddEquipment(JSONString));
            break;
    }
}

