namespace Booking.Domain.Rooms
{
    public class Room : Entity
    {
        public RoomId RoomId { get; }
        private List<Schedule> schedules;
        private int roomCapacity;

        public Room(RoomId roomId, List<Schedule> schedules, int roomCapacity)
        {
            RoomId = roomId;
            this.schedules = schedules;
            this.roomCapacity = roomCapacity;
        }

        internal bool IsAvailable(Schedule schedule)
        {
            return !schedules.Any(currentSchedule => schedule.IsInInterval(currentSchedule));
        }

        internal bool HasNotCapacity(int countPersons) 
                => roomCapacity < countPersons;
    }
}
