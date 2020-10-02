using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Realtor.Application.Features.Location;
using Realtor.Core.Extensions;

namespace Realtor.Api.Controllers
{
    public class LocationController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync([FromBody] CreateLocation.Command command)
        {
            Guid id = await Mediator.Send(command).AnyContext();

            return StatusCode(200, id);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetLocations.Model), StatusCodes.Status200OK)]
        public Task<IEnumerable<GetLocations.Model>> Get()
        {
            return Mediator.Send(new GetLocations.Query());
        }

        [HttpPost("DeleteLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteLocation([FromBody] DeleteLocation.Command command)
        {
            Guid id = await Mediator.Send(command).AnyContext();

            return StatusCode(200, id);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostAsync([FromBody] UpdateLocation.Command command)
        {
            Guid id = await Mediator.Send(command).AnyContext();

            return StatusCode(200, id);
        }
    }
}
