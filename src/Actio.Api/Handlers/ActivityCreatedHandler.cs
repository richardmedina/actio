using Actio.Api.Models;
using Actio.Api.Repositories;
using Actio.Common.Events;
using Actio.Common.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new ActivityModel
            {
                Id = @event.Id,
                UserId = @event.UserId,
                Name = @event.Name,
                Category = @event.Category,
                Description = @event.Description,
                CreatedAt = @event.CreatedAt
            });

            Console.WriteLine($"Activity Created: {@event.Name}");
        }
    }
}
