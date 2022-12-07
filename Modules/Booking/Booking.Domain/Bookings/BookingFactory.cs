using Booking.Domain.Customers;
using Booking.Domain.Rooms;

namespace Booking.Domain.Bookings
{
    public class BookingFactory
    {
        private readonly IBusinessRulesEngine<CreateBookingRequest> businessRulesEngine;
        public BookingFactory(IBusinessRulesEngine<CreateBookingRequest> businessRulesEngine)
        {
            this.businessRulesEngine = businessRulesEngine;
        }

        public Booking Create(CreateBookingRequest request)
        {
            businessRulesEngine.CheckRules(request);

            return Booking.Create(request.Room.RoomId,
                                  request.StartDate,
                                  request.EndDate,
                                  request.Customer.Id);
        }

    }

    public record CreateBookingRequest(Room Room,
                                       DateTime StartDate,
                                       DateTime EndDate,
                                       int CountPersons,
                                       Customer Customer);
}
