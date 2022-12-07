using Booking.Domain.Bookings;

namespace Booking.Infrastructure.Repository
{
    public class InMemoryBookingRepository : IBookingRepository
    {
        private readonly List<Domain.Bookings.Booking> _books;
        public InMemoryBookingRepository(List<Domain.Bookings.Booking> bookings = null!)
        {
            _books = bookings ?? new List<Domain.Bookings.Booking>();
        }

        public IEnumerable<Domain.Bookings.Booking> Bookings { get => _books; }

        public Task SaveAsync(Domain.Bookings.Booking booking)
        {
            _books.Add(booking);
            return Task.CompletedTask;
        }
    }
}
