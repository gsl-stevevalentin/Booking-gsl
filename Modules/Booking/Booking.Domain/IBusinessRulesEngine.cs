namespace Booking.Domain
{
    public interface IBusinessRulesEngine<T>
    {
        void CheckRules(T request);
    }
}
