using Actio.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Actio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IBusClient _busClient;
        public CategoriesController(IBusClient busClient)
        {
            _busClient = busClient;
        }
        public async Task<IActionResult> Post(CreateCategory command)
        {
            command.Id = Guid.NewGuid();
            await _busClient.PublishAsync(command);

            return Accepted(command.Id);
        }
    }
}
