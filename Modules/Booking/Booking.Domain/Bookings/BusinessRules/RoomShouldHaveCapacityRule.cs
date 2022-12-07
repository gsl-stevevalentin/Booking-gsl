using Booking.Domain.Bookings.Exceptions;

namespace Booking.Domain.Bookings.BusinessRules
{
    public class RoomShouldHaveCapacityRule : IBusinessRule<CreateBookingRequest>
    {
        public void Check(CreateBookingRequest request)
        {
            if (request.Room.HasNotCapacity(request.CountPersons))
                throw new RoomCapacityExceededException();
        }
    }
}
