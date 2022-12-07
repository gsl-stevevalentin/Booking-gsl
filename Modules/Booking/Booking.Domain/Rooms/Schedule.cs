using Booking.Domain.Bookings.Exceptions;

namespace Booking.Domain.Rooms
{
    public record Schedule()
    {
        private const int MaxHoursAuthorized = 48;

        public Schedule(DateTime start, DateTime end) : this()
        {
            if (end.Subtract(start).TotalHours > MaxHoursAuthorized)
                throw new ScheduleTooLargeException();

            Start = start;
            End = end;
        }

        public DateTime Start { get; }
        public DateTime End { get; }

        internal bool IsInInterval(Schedule schedule) =>
            IsInStartInterval(schedule) ||
            IsInEndInterval(schedule);

        private bool IsInEndInterval(Schedule schedule)
            => schedule.Start < End && End <= schedule.End;

        private bool IsInStartInterval(Schedule schedule) =>
            schedule.Start <= Start && Start < schedule.End;
    }
}
