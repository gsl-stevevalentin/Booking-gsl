namespace Booking.Domain.Bookings.Events
{
    public record BookingCreated(BookingId BookingId) : IDomainEvent;
}
