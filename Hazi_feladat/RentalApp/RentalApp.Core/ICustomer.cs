
namespace RentalApp.Core;

internal interface ICustomer : IHasIdentifier
{
    string Name { get; init; }
    string Email {  get; init; }
    string Phone { get; init; } 
    

}
