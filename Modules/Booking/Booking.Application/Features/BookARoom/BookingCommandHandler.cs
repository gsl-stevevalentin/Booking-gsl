using Booking.Application.Abstractions;
using Booking.Domain.Bookings;
using Booking.Domain.Customers;
using Booking.Domain.Customers.Exceptions;
using Booking.Domain.Rooms;
using Booking.Domain.Rooms.Exceptions;
using MediatR;

namespace Booking.Application.Features.BookARoom
{
    public sealed class BookingCommandHandler : ICommandHandler<BookingCommand>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IAuthenticationService authenticationService;
        private readonly ICustomerRepository customerRepository;
        private readonly BookingFactory bookingFactory;

        public BookingCommandHandler(IRoomRepository roomRepository,
                                    IBookingRepository bookingRepository,
                                    IAuthenticationService authenticationService,
                                    ICustomerRepository customerRepository,
                                    BookingFactory bookingFactory)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
            this.authenticationService = authenticationService;
            this.customerRepository = customerRepository;
            this.bookingFactory = bookingFactory;
        }

        public async Task<Unit> Handle(BookingCommand request, CancellationToken cancellationToken = default)
        {
            var room = await _roomRepository.GetAsync(new RoomId(request.RoomId)) ??
                            throw new RoomDoesNotExistException();

            var customer = await customerRepository.GetAsync(new CustomerId(authenticationService.GetCustomerId()));

            var booking = bookingFactory.Create(new CreateBookingRequest(room,
                                                                        request.StartDate,
                                                                        request.EndDate,
                                                                        request.CountPersons,
                                                                        customer));
            await _bookingRepository.SaveAsync(booking);

            return Unit.Value;
        }
    }
}
