using Booking.Application.Features.BookARoom;
using Booking.Domain;
using Booking.Domain.Bookings;
using Booking.Domain.Bookings.BusinessRules;
using Booking.Domain.Customers;
using Booking.Domain.Rooms;
using Booking.Infrastructure.Repository;
using Boonking.UnitTest.Adapters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Boonking.UnitTest.SUTs
{
    internal class BookHandlerSUT
    {
        private readonly Guid roomId;
        private readonly Guid userId;
        private readonly InMemoryBookingRepository bookingRepository;
        private InMemoryRoomRepository roomRepository;
        private FakeAuthenticationService authenticationService;
        private InMemoryCustomerRepository customerRepository;
        private readonly BookingFactory bookingFactory;

        public BookHandlerSUT(Guid roomId)
        {
            this.roomId = roomId;
            userId = Guid.NewGuid();
            bookingRepository = new InMemoryBookingRepository();
            roomRepository = new InMemoryRoomRepository();
            customerRepository = new InMemoryCustomerRepository();
            authenticationService = new FakeAuthenticationService(userId);
            var createBookingBusinessRulesEngine = new CreateBookingBusinessRulesEngine(new List<IBusinessRule<CreateBookingRequest>>
            {
                new CustomerShouldExistRule(),
                new RoomShouldBeAvailableRule(),
                new RoomShouldHaveCapacityRule()
            });
            bookingFactory = new BookingFactory(createBookingBusinessRulesEngine);
        }

        internal BookHandlerSUT ArrangeRepositories(Room room)
        {
            roomRepository = new InMemoryRoomRepository(new List<Room>() { room });
            customerRepository = new InMemoryCustomerRepository(Customer.FromMemento(new CustomerMemento(userId)));
            return this;
        }

        internal BookHandlerSUT ArrangeUser(Guid userId)
        {
            authenticationService = new FakeAuthenticationService(userId);
            return this;
        }

        internal async Task ActHandlerAsync(BookingCommand bookingCommand)
        {
            var handler = new BookingCommandHandler(roomRepository,
                                                    bookingRepository,
                                                    authenticationService,
                                                    customerRepository,
                                                    bookingFactory);
            await handler.Handle(bookingCommand);
        }

        internal async Task ActHandlerThatNotSaveBookingAsync(BookingCommand bookingCommand)
        {
            var handler = new BookingCommandHandler(roomRepository,
                                                    bookingRepository,
                                                    authenticationService,
                                                    customerRepository,
                                                    bookingFactory);
            await Record.ExceptionAsync(() => handler.Handle(bookingCommand));
        }

        internal void AssertThatBookingIsSaved()
        {
            Assert.NotEmpty(bookingRepository.Bookings);
        }

        internal void AssertThatBookingIsNotSaved()
        {
            Assert.Empty(bookingRepository.Bookings);
        }
    }
}
