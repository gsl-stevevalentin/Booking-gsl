namespace Booking.Domain.Customers
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAsync(CustomerId customerId);
    }
}
