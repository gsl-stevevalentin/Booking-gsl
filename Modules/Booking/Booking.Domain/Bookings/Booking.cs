using Booking.Domain.Bookings.Events;
using Booking.Domain.Customers;
using Booking.Domain.Rooms;

namespace Booking.Domain.Bookings
{
    public class Booking : Entity, IAggregateRoot
    {
        private readonly RoomId _roomId;
        private readonly CustomerId _customerId;
        private readonly Schedule _period;
        private readonly BookingId _bookingId;

        private Booking(BookingId bookingId, RoomId roomId, DateTime startDate, DateTime endDate, CustomerId customerId)
        {
            _bookingId = bookingId;
            _roomId = roomId;
            _customerId = customerId;
            _period = new(startDate, endDate);
            AddDomainEvent(new BookingCreated(_bookingId));
        }

        internal static Booking Create(RoomId roomId, DateTime startDate, DateTime endDate, CustomerId customerId)
        {
            return new(new(Guid.NewGuid()), roomId, startDate, endDate, customerId);
        }
    }
}
