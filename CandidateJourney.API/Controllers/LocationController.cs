using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CandidateJourney.API.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationController : ControllerBase
    {
        private readonly IRestLocationService _locationService;

        public LocationController(IRestLocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all locations", Description = "Returns all locations")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> GetAllLocations([FromQuery] int pageNumber, [FromQuery] string? filterString)
        {
            var locations = await _locationService.GetAllLocationsAsync(pageNumber, filterString);
            return Ok(locations);
        }

        [Route("{locationId}")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get a location by id", Description = "Returns location by given id")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> GetLocation(Guid locationId)
        {
            var foundLocation = await _locationService.GetLocationByIdAsync(locationId);
            return Ok(foundLocation);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Registers a location to the database", Description = "Registers a location to the database")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        public async Task<ActionResult> AddLocation([FromBody] CreateLocationCommand command)
        {
            var validator = new CreateLocationCommandValidator();
            await validator.ValidateAndThrowAsync(command);

            var newLocation = await _locationService.AddLocationAsync(command);
            return CreatedAtAction(nameof(AddLocation), new { id = newLocation.Id }, newLocation);
        }

        [Route("{locationId}")]
        [HttpPut]
        [SwaggerOperation(Summary = "Updates a location", Description = "Updates a location")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> UpdateLocation(Guid locationId, [FromBody] UpdateLocationCommand command)
        {
            try
            {
                var validator = new UpdateLocationCommandValidator();
                await validator.ValidateAndThrowAsync(command);
                var updatedLocation = await _locationService.UpdateLocationAsync(locationId, command);
                return Ok(updatedLocation);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Route("{locationId}")]
        [HttpDelete]
        [SwaggerOperation(Summary = "Deletes a location", Description = "Deletes a location")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> DeleteLocation(Guid locationId)
        {
            try
            {
                var deletedLocation = await _locationService.DeleteLocationAsync(locationId);
                return Ok(deletedLocation);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
