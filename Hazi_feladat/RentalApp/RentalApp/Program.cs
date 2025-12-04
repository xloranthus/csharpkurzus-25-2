

using RentalApp.Core;
using App = RentalApp.Core.RentalApp;


// TODO Fix:
// const string basePath = @"C:\Users\xloranthus\source\repos\csharpkurzus-25-2\Hazi_feladat\RentalApp\RentalApp";
const string basePath = @"C:\Users\wivie\source\repos\csharpkurzus-25-2\Hazi_feladat\RentalApp\RentalApp";
string equipmentDBFile = Path.Combine(basePath, "equipmentDB.json");
string customerDBFile = Path.Combine(basePath, "customerDB.json");
string reservationDBFile = Path.Combine(basePath, "reservationDB.json");

IRentalApp app = new App(equipmentDBFile, customerDBFile, reservationDBFile);

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

