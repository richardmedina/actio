using Actio.Common.Events;
using Actio.Common.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Handlers
{
    public class UserAuthenticatedHandler : IEventHandler<UserAuthenticated>
    {
        public async Task HandleAsync(UserAuthenticated @event)
        {
            await Task.CompletedTask;
            Console.WriteLine($"User Authenticated {@event.Email}");
        }
    }
}
