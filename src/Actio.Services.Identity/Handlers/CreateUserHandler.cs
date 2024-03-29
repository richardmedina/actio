﻿using Actio.Common.Commands;
using Actio.Common.Events;
using Actio.Common.Exceptions;
using Actio.Common.Handlers;
using Actio.Services.Identity.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public CreateUserHandler(IBusClient busClient, IUserService userService, ILogger<CreateUserHandler> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateUser command)
        {
            _logger.LogInformation($"Creating User: {command.Email} {command.Name}");

            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);

                await _busClient.PublishAsync(new UserCreated(command.Email, command.Name));
            }
            catch (ActionException ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, ex.Code, ex.Message));
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                await _busClient.PublishAsync(new CreateUserRejected(command.Email, "error", ex.Message));
                _logger.LogError(ex.Message);
            }
        }
    }
}
