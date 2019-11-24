using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Handlers;
using Actio.Services.Activities.Domain.Models;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Services;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Activities.Handlers
{
    public class CreateCategoryHandler : ICommandHandler<CreateCategory>
    {
        public readonly ICategoryService _categoryService;
        private readonly IBusClient _busClient;

        public CreateCategoryHandler(ICategoryService categoryService, IBusClient busClient)
        {
            _categoryService = categoryService;
            _busClient = busClient;
        }
        public async Task HandleAsync(CreateCategory command)
        {
            await _categoryService.AddAsync(command.Id, command.Name);
            await _busClient.PublishAsync(new CategoryCreated(command.Id, command.Name));
        }
    }
}
