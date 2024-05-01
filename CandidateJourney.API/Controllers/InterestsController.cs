using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CandidateJourney.API.Controllers
{
    [Route("api/interests")]
    [ApiController]
    public class InterestsController : ControllerBase
    {
        private readonly IInterestService _interestService;

        public InterestsController(IInterestService interestService)
        {
            _interestService = interestService;
        }

        [HttpGet("GetAllInterestNames")]
        [SwaggerOperation(Summary = "Get all interest names", Description = "Returns all interest names")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> GetAllInterestsName()
        {
            var interests = await _interestService.GetAllInterestNamesAsync();
            return Ok(interests);
        }

        [HttpGet("GetAllInterests")]
        [SwaggerOperation(Summary = "Get all interests", Description = "Returns all interests")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> GetAllInterests()
        {
            var interests = await _interestService.GetAllInterestsAsync();
            return Ok(interests);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get interest by id", Description = "Returns interest by id")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> GetInterestById(int id)
        {
            var interest = await _interestService.GetInterestByIdAsync(id);
            if (interest == null)
            {
                return NotFound();
            }
            return Ok(interest);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Add an interest", Description = "Adds an interest")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> AddInterest([FromBody] CreateInterestCommand command)
        {
            var validator = new CreateInterestCommandValidator();
            await validator.ValidateAndThrowAsync(command);

            var newInterest = await _interestService.AddInterestAsync(command);
            return CreatedAtAction(nameof(AddInterest), new { id = newInterest.Id }, newInterest);
        }

        [HttpPut("{interestId}")]
        [SwaggerOperation(Summary = "Update an interest", Description = "Updates an interest")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> UpdateInterest(int interestId, [FromBody] UpdateInterestCommand command)
        {
            var validator = new UpdateInterestCommandValidator();
            await validator.ValidateAndThrowAsync(command);
            var interest = await _interestService.GetInterestByIdAsync(interestId);
            if (interest == null)
            {
                return NotFound();
            }
            var updatedInterest = await _interestService.UpdateInterestAsync(interestId, command);
            return Ok(updatedInterest);
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an interest", Description = "Deletes an interest")]
        [SwaggerResponse(204, "Ok")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> DeleteInterest(int id)
        {
            var interest = await _interestService.GetInterestByIdAsync(id);
            if (interest == null)
            {
                return NotFound();
            }
            await _interestService.DeleteInterestAsync(id);
            return NoContent();
        }
    }
}
