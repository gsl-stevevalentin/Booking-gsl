using Booking.Domain.Rooms;

namespace Booking.Infrastructure.Repository
{
    public class InMemoryRoomRepository : IRoomRepository
    {
        private readonly List<Room> _rooms;

        public InMemoryRoomRepository(List<Room> rooms = null!)
        {
            _rooms = rooms ?? new List<Room>();
        }

        public Task<Room> GetAsync(RoomId roomId)
        {
            return Task.FromResult(_rooms.FirstOrDefault(x => x.RoomId == roomId))!;
        }
    }
}
