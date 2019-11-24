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
        public ActivitiesController(IBusClient busClient, ILogger<ActivitiesController> logger)
        {
            _busClient = busClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            await Task.CompletedTask;
            _logger.LogInformation("ActivitiesController.GetAll with userId: ", User.Identity.Name);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.CreatedAt = DateTime.UtcNow;
            await _busClient.PublishAsync(command);

            return Accepted($"activities/{command.Id}");
        }

        [HttpGet("secure")]
        public IActionResult GetSecure() => Content("Hello Secure World!");
    }
}
