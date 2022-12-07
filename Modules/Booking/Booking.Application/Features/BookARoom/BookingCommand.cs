using Booking.Application.Abstractions;

namespace Booking.Application.Features.BookARoom
{
    public sealed record BookingCommand(Guid RoomId, DateTime StartDate, DateTime EndDate, int CountPersons) : ICommand;
}
