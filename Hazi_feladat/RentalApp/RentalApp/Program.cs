

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
    string cmdParam = cmds[1];
    
    switch (cmdType)
    {
        case "addeq":
        case "updeq":
            if (File.Exists(cmdParam) is false)
            {
                Console.WriteLine($"{cmdParam} does not exist.");
                Console.Write("$>");
                continue;
            }
            string JSONString = File.ReadAllText(cmdParam);
            switch (cmdType)
            {
                case "addeq":
                    Console.WriteLine(app.AddEquipment(JSONString));
                    break;
                case "updeq":
                    Console.WriteLine(app.UpdateEquipment(JSONString));
                    break;
            }
            break;
        case "deleq":
            Console.WriteLine(app.DeleteEquipment(cmdParam));
            break;

    }

    Console.Write("$>");
}

