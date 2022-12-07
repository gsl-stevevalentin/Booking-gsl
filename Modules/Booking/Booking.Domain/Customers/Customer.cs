namespace Booking.Domain.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        private Customer(CustomerId customerId)
        {
            Id = customerId;
        }
        public static Customer Default { get => default!; }
        public CustomerMemento Memento { get => new CustomerMemento(Id.Value); }
        internal CustomerId Id { get; }

        public static Customer FromMemento(CustomerMemento customerMemento)
        {
            return new(new(customerMemento.CustomerId));
        }
    }
}
