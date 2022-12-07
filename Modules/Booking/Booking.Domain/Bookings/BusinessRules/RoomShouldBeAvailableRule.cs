using Booking.Domain.Rooms;
using Booking.Domain.Rooms.Exceptions;

namespace Booking.Domain.Bookings.BusinessRules
{
    public class RoomShouldBeAvailableRule : IBusinessRule<CreateBookingRequest>
    {
        public void Check(CreateBookingRequest request)
        {
            if (!request.Room.IsAvailable(new Schedule(request.StartDate, request.EndDate)))
                throw new RoomNotAvailableException();
        }
    }
}
