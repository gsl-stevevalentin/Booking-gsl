using MediatR;

namespace Booking.Application.Abstractions
{
    public interface ICommandHandler<T> : IRequestHandler<T> where T : ICommand
    {
    }

    public interface ICommand : IRequest<Unit> { }
}
