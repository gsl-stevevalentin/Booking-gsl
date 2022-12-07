namespace Booking.Domain
{
    public interface IBusinessRule<T>
    {
        void Check(T request);
    }
}
