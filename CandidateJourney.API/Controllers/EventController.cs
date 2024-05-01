﻿using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;

namespace CandidateJourney.API.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IBlobService _blobService;

        public EventController(IEventService eventService, IBlobService blobService)
        {
            _eventService = eventService;
            _blobService = blobService;
        }

        [HttpGet("GetAllUpcomingEvents")]
        [SwaggerOperation(Summary = "Get all upcoming events", Description = "Returns all upcoming events")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> GetAllUpcomingEvents([FromQuery] int pageNumber)
        {
            var events = await _eventService.GetAllUpcomingEventsAsync(pageNumber, "");
            return Ok(events);
        }

        [HttpGet("GetAllPreviousEvents")]
        [SwaggerOperation(Summary = "Get all previous events", Description = "Returns all previous events")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> GetAllPreviousEvents([FromQuery] int pageNumber)
        {
            var events = await _eventService.GetAllPreviousEventsAsync(pageNumber, "");
            return Ok(events);
        }

        [HttpGet("FilterAllUpcomingEvents")]
        [SwaggerOperation(Summary = "Filter all upcoming events", Description = "Returns all upcoming events filtered by a string")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> FilterAllUpcomingEvents([FromQuery] int pageNumber, [FromQuery] string filterString)
        {
            var events = await _eventService.GetAllUpcomingEventsAsync(pageNumber, filterString);
            return Ok(events);
        }

        [HttpGet("FilterAllPreviousEvents")]
        [SwaggerOperation(Summary = "Filter all previous events", Description = "Returns all previous events filtered by a string")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> FilterAllPreviousEvents([FromQuery] int pageNumber, [FromQuery] string filterString)
        {
            var events = await _eventService.GetAllPreviousEventsAsync(pageNumber, filterString);
            return Ok(events);
        }
        [HttpPost]
        [SwaggerOperation(Summary = "Registers an event to the database", Description = "Registers an event to the database")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        public async Task<ActionResult> AddEvent([FromBody] CreateEventCommand command)
        {
            var validator = new CreateEventCommandValidator();
            await validator.ValidateAndThrowAsync(command);

            var newEvent = await _eventService.AddEventAsync(command);
            return CreatedAtAction(nameof(AddEvent), new { id = newEvent.Id }, newEvent);
        }

        [Route("{eventId}")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get an event by id", Description = "Returns event by given id")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> GetEvent(Guid eventId)
        {
            var foundEvent = await _eventService.GetEventByIdAsync(eventId);
            return Ok(foundEvent);
        }

        [Route("{eventId}/candidates")]
        [HttpPost]
        [SwaggerOperation(Summary = "Registers a candidate to an event to the database", Description = "Registers a candidate to an event to the database")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        public async Task<ActionResult> AddCandidateToEvent(Guid eventId, [FromBody] CreateCandidateCommand command)
        {
            var validator = new CreateCandidateCommandValidator();
            await validator.ValidateAndThrowAsync(command);
            var newCandidate = await _eventService.AddCandidateToEventAsync(eventId, command);

            return CreatedAtAction(nameof(AddCandidateToEvent), new { id = newCandidate.Id }, newCandidate);
        }


        [Route("{eventId}/candidates")]
        [HttpGet]
        [SwaggerOperation(Summary = "Gets all candidates of an event", Description = "Returns all candidates of an event")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> GetEventByIdWithCandidates(Guid eventId)
        {
            var eventWithCandidates = await _eventService.GetEventByIdAsync(eventId);
            return Ok(eventWithCandidates);
        }
        [Route("{eventId}/candidates/{candidateId}")]
        [HttpGet]
        [SwaggerOperation(Summary = "Gets candidate of an event", Description = "Returns candidate of an event")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> GetCandidateById(Guid eventId, Guid candidateId)
        {
            var candidate = await _eventService.GetCandidateByIdAsync(eventId, candidateId);
            candidate.ContactHistories.ForEach(h => h.CreatedOn = h.CreatedOn.ToLocalTime());
            return Ok(candidate);
        }

        [HttpGet]
        [Route("exportCsv/{eventId}")]
        [SwaggerOperation(Summary = "Gets all candidates of an event", Description = "Returns all candidates of an event")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> Export(Guid eventId)
        {
            var builder = await _eventService.GetCandidateExportByEventIdAsync(eventId);
            return File(Encoding.UTF8.GetBytes(builder), "text/csv", "Candidates.csv");
        }

        [Route("candidates/pictures/{pictureName}")]
        [HttpGet]
        [SwaggerOperation(Summary = "Gets all picture of an candidate",
            Description = "Returns SAS uri of a candidate picture")]
        [SwaggerResponse(200, "Ok")]
        public async Task<IActionResult> GetPhoto(string pictureName)
        {
            var imageUri = await _blobService.GetBlobUri(pictureName);
            return Ok(new { link = imageUri });
        }


        [Route("{eventId}")]
        [HttpPut]
        [SwaggerOperation(Summary = "Updates an event", Description = "Updates an event")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> ChangeEvent(Guid eventId, [FromBody] UpdateEventCommand command)
        {
            try
            {
                var validator = new UpdateEventCommandValidator();
                await validator.ValidateAndThrowAsync(command);
                var updatedEvent = await _eventService.ChangeEventAsync(eventId, command);
                return Ok(updatedEvent);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Route("archive/{eventId}")]
        [HttpDelete]
        [SwaggerOperation(Summary = "Archive an event", Description = "Archive an event")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<IActionResult> ArchiveEvent(Guid eventId)
        {
            try
            {
                var updatedEvent = await _eventService.ArchiveEventAsync(eventId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [Route("{eventId}/candidates/{candidateId}")]
        [HttpDelete]
        [SwaggerOperation(Summary = "Deletes a candidate from an event from the database", Description = "Deletes a candidate from an event from the database")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        public async Task<ActionResult> DeleteCandidateFromEvent(Guid eventId, Guid candidateId)
        {
            try
            {
                var updatedEvent = await _eventService.RemoveCandidateByIdAsync(eventId, candidateId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
