
namespace RentalApp.Core;

public class Sport
{

    public required string Value { get; init; }

    public static Sport Skiing { get { return new Sport() { Value = "Skiing"}; } }
    public static Sport Snowboarding { get { return new Sport() { Value = "Snowboarding" }; } }
    // Cycling
    // IceSkating

    public override string ToString()
    {
        return Value; 
    }
}
