using Booking.Domain.Customers;
using static Booking.Domain.Customers.Customer;

namespace Booking.Infrastructure.Repository
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private readonly Customer _customer;

        public InMemoryCustomerRepository(Customer customer = null!)
        {
            _customer = customer;
        }
        public Task<Customer> GetAsync(CustomerId customerId)
        {
            if (_customer.Memento.CustomerId == customerId.Value)
                return Task.FromResult(_customer);

            return Task.FromResult(Default);
        }
    }
}
