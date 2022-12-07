using Booking.Domain.Customers;
using Booking.Domain.Customers.Exceptions;

namespace Booking.Domain.Bookings.BusinessRules
{
    public class CustomerShouldExistRule : IBusinessRule<CreateBookingRequest>
    {
        public void Check(CreateBookingRequest request)
        {
            if (request.Customer == Customer.Default)
                throw new CustomerUnknownException();
        }
    }
}
