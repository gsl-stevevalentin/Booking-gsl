using Booking.Domain.Bookings;

namespace Booking.Domain
{
    public class CreateBookingBusinessRulesEngine : IBusinessRulesEngine<CreateBookingRequest>
    {
        private readonly IEnumerable<IBusinessRule<CreateBookingRequest>> businessRules;

        public CreateBookingBusinessRulesEngine(IEnumerable<IBusinessRule<CreateBookingRequest>> businessRules)
        {
            this.businessRules = businessRules;
        }
        public void CheckRules(CreateBookingRequest request)
        {
            foreach (var rule in businessRules)
                rule.Check(request);
        }
    }
}
