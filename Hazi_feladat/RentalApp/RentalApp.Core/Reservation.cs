
using System.Diagnostics;

namespace RentalApp.Core;

internal class Reservation : IReservation
{

    private static long _nextId = 0;

    private readonly long _reservationID = _nextId++;
    
    public string Identifier => _reservationID.ToString();

    public long ReservationID
    {
        get
        {
            return _reservationID; 
        }
        init
        {
            if(value < 0)
            {
                throw new UnreachableException("Reservation ID is in an invalid state.");
            }
            _reservationID = value;
        }
            
    }
    public required string CustomerEmail { get; init; }
    public required (DateOnly start, DateOnly end) ReservationPeriod { get; init; }
    public int LengthInDays => ReservationPeriod.end.DayNumber - ReservationPeriod.end.DayNumber;
    public required IReadOnlyList<string> Barcodes { get; init; }

}
