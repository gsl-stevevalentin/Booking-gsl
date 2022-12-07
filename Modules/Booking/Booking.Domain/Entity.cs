namespace Booking.Domain
{
    public abstract class Entity
    {
        private List<IDomainEvent> _domainEvents;

        public Entity()
        {
            _domainEvents = new List<IDomainEvent>();
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
