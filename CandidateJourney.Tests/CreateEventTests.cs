using AutoMapper;
using CandidateJourney.Application.Contracts.Commands;
using CandidateJourney.Application.MappingProfiles;
using CandidateJourney.Application.Services;
using CandidateJourney.Domain;
using CandidateJourney.Infrastructure.Repositories;
using Moq;
using Xunit;

namespace CandidateJourney.Tests;

public class CreateEventTests
{
    [Fact]
    public async void AddNewCandidateToEvent()
    {
        // Mock IEventRepo
        var eventRepositoryMock = new Mock<IEventRepository>();
        // Setup methods
        var @event = new Event("Name", "Organizer", "Location", DateTime.Now, null, AudienceCategory.All, "", "");
        eventRepositoryMock.Setup(repo => repo.UpdateEvent(It.IsAny<Event>())).Returns((Event e) => Task.FromResult(e));
        eventRepositoryMock.Setup(repo => repo.FindById(It.IsAny<Guid>())).Returns(Task.FromResult(@event));
        // Create EventService with mapper and EventRepo mock
        var blobServiceMock = new Mock<IBlobService>();
        var eventService = new EventService(CreateMapper(), eventRepositoryMock.Object, blobServiceMock.Object);
        // Call method on EventService
        var createCandidateCommand = new CreateCandidateCommand()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "FirstName.LastName@email.com",
            CandidateType = CandidateIntent.Internship,
            Interests = new List<string>() { "Microsoft Development" }
        };
        var eventModel = await eventService.AddCandidateToEventAsync(@event.Id, createCandidateCommand);
        // Assert stuff on result
        Assert.True(eventModel.Candidates.Count > 0);
        Assert.Equal("FirstName", eventModel.Candidates[0].FirstName);
        Assert.Equal("LastName", eventModel.Candidates[0].LastName);
        Assert.Equal("FirstName.LastName@email.com", eventModel.Candidates[0].Email);
        Assert.Equal(CandidateIntent.Internship, eventModel.Candidates[0].CandidateType);
        Assert.Equal("Microsoft Development", eventModel.Candidates[0].Interests[0]);
    }



    private IMapper CreateMapper()
    {
        var mappingConfig = new MapperConfiguration(cofig =>
        {
            cofig.AddProfile<EventMappingProfile>();
            cofig.AddProfile<CandidateMappingProfile>();
        });
        var mapper = new Mapper(mappingConfig);
        return mapper;
    }
}