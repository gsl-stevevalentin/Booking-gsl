namespace Booking.Domain.Rooms
{
    public interface IRoomRepository
    {
        Task<Room> GetAsync(RoomId roomId);
    }
}
