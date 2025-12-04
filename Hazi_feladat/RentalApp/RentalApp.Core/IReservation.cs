
namespace RentalApp.Core;

internal interface IReservation : IHasIdentifier
{
    long ReservationID { get; init; }
    string CustomerEmail { get; init; }
    (DateOnly start, DateOnly end) ReservationPeriod {  get; init; }
    int LengthInDays { get; }
    IReadOnlyList<string> Barcodes { get; init; }
}
