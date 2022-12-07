namespace Booking.Domain.Bookings
{
    public interface IBookingRepository
    {
        Task SaveAsync(Booking booking);
    }
}
