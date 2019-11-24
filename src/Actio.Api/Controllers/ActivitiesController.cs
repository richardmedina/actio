using Actio.Api.Repositories;
using Actio.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IBusClient _busClient;
        private readonly ILogger _logger;

        private readonly IActivityRepository _activityRepository;

        public ActivitiesController(IBusClient busClient, IActivityRepository activityRepository, ILogger<ActivitiesController> logger)
        {
            _busClient = busClient;
            _logger = logger;
            _activityRepository = activityRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Accesing ActivitiesController.GetAll({User.Identity.Name})");
            var activities = await _activityRepository.BrowseAsync(Guid.Parse(User.Identity.Name));

            return Ok(activities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            _logger.LogInformation($"Accesing ActivitiesController.GetAll({User.Identity.Name})");
            var activity = await _activityRepository.GetAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.UserId = Guid.Parse(User.Identity.Name);
            command.CreatedAt = DateTime.UtcNow;
            await _busClient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }

        [HttpGet("secure")]
        public IActionResult GetSecure() => Content("Hello Secure World!");
    }
}
