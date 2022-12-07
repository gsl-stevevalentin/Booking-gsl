using Booking.Application.Abstractions;
using System;

namespace Boonking.UnitTest.Adapters
{
    internal class FakeAuthenticationService : IAuthenticationService
    {
        private Guid accountId;

        public FakeAuthenticationService(Guid accountId)
        {
            this.accountId = accountId;
        }

        public Guid GetCustomerId() => accountId;
    }
}
