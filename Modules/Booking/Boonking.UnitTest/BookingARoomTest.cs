using Booking.Application.Features.BookARoom;
using Booking.Domain.Rooms;
using Boonking.UnitTest.SUTs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Boonking.UnitTest
{
    public class BookingARoomTest
    {
        private readonly Guid roomId;
        private readonly BookHandlerSUT sut;

        public BookingARoomTest()
        {

            roomId = Guid.NewGuid();
            sut = new BookHandlerSUT(roomId);
        }
        [Fact]
        public async Task SHouldBookingARoom()
        {
            sut.ArrangeRepositories(new Room(new RoomId(roomId), new List<Schedule>(), 2));
            await sut.ActHandlerAsync(new BookingCommand(roomId, new DateTime(2022, 12, 15), new DateTime(2022, 12, 17), 2));
            sut.AssertThatBookingIsSaved();
        }

        [Fact]
        public async Task ShouldNotBookARoomWhenRoomIsNotExist()
        {
            await sut.ActHandlerThatNotSaveBookingAsync(new BookingCommand(roomId,
                                                         new DateTime(2022, 12, 15),
                                                         new DateTime(2022, 12, 17),
                                                         2));
            sut.AssertThatBookingIsNotSaved();

        }

        [Fact]
        public async Task ShouldNotBookWhenRoomIsNotAvailableForSameSchedule()
        {
            var room = new Room(new(roomId),
                                new() { new(new DateTime(2022, 12, 15), new DateTime(2022, 12, 17)) },
                                2);
            sut.ArrangeRepositories(room);
            await sut.ActHandlerThatNotSaveBookingAsync(new(roomId, new DateTime(2022, 12, 15), new DateTime(2022, 12, 17), 2));
            sut.AssertThatBookingIsNotSaved();
        }

        [Fact]
        public async Task ShouldNotBookWhenRoomIsNotAvailableAtStartDate()
        {
            var room = new Room(new(roomId),
                                new() { new(new DateTime(2022, 12, 15), new DateTime(2022, 12, 17)) },
                                2);
            sut.ArrangeRepositories(room);
            await sut.ActHandlerThatNotSaveBookingAsync(new(roomId, new DateTime(2022, 12, 16), new DateTime(2022, 12, 17), 2));
            sut.AssertThatBookingIsNotSaved();
        }

        [Fact]
        public async Task ShouldNotBookWhenRoomIsNotAvailableAtEndDate()
        {
            var room = new Room(new(roomId),
                                new() { new(new DateTime(2022, 12, 15), new DateTime(2022, 12, 17)) },
                                2);
            sut.ArrangeRepositories(room);
            await sut.ActHandlerThatNotSaveBookingAsync(new(roomId, new DateTime(2022, 12, 14), new DateTime(2022, 12, 16), 2));
            sut.AssertThatBookingIsNotSaved();
        }

        [Theory]
        [InlineData("2022-12-17", "2022-12-19")]
        [InlineData("2022-12-14", "2022-12-15")]
        public async Task ShouldBookWhenRoomIsAvailableForSchedule(string start, string end)
        {
            var room = new Room(new(roomId),
                                new() { new(new DateTime(2022, 12, 15), new DateTime(2022, 12, 17)) },
                                2);
            sut.ArrangeRepositories(room);
            await sut.ActHandlerAsync(new(roomId, DateTime.Parse(start), DateTime.Parse(end), 2));
            sut.AssertThatBookingIsSaved();
        }

        [Fact]
        public async Task ShouldNotBookWhenScheduleIsMoreThan48h()
        {
            var room = new Room(new(roomId), new List<Schedule>() { new(new DateTime(2022, 12, 17), new DateTime(2022, 12, 19)) }, 2);
            sut.ArrangeRepositories(room);
            await sut.ActHandlerThatNotSaveBookingAsync(new(roomId, new DateTime(2022, 11, 16), new DateTime(2022, 11, 20), 2));
            sut.AssertThatBookingIsNotSaved();
        }

        [Fact]
        public async Task ShouldNotBookWhenCountPeopleIsMoreThanRoomCapacity()
        {
            var room = new Room(new(roomId), new List<Schedule>() { new(new DateTime(2022, 12, 17), new DateTime(2022, 12, 19)) }, 2);
            sut.ArrangeRepositories(room);
            await sut.ActHandlerThatNotSaveBookingAsync(new(roomId, new DateTime(2022, 11, 16), new DateTime(2022, 11, 17), 4));
            sut.AssertThatBookingIsNotSaved();
        }

        [Fact]
        public async Task ShouldNotBookWhenAccountIdIsUnknown()
        {
            var room = new Room(new(roomId), new List<Schedule>() { new(new DateTime(2022, 12, 17), new DateTime(2022, 12, 19)) }, 2);
            sut.ArrangeRepositories(room)
               .ArrangeUser(Guid.Empty);
            await sut.ActHandlerThatNotSaveBookingAsync(new(roomId, new DateTime(2022, 11, 16), new DateTime(2022, 11, 16), 2));
            sut.AssertThatBookingIsNotSaved();
        }
    }
}