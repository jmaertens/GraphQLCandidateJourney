using CandidateJourney.Domain;
using CandidateJourney.Infrastructure.Repositories;
using Moq;

namespace CandidateJourney.Tests.Mocks;

public static class EventRepositoryMock
{
    public static List<Event> Events { get; set; } = new List<Event>
    {
        new Event("name", "organizer", DateTime.Now, DateTime.Now.AddDays(5), AudienceCategory.Student, "description", "https://ordina.be"),
        new Event("event2", "organizer2", DateTime.Now, DateTime.Now.AddDays(10), AudienceCategory.All, "description2", "https://ordina.be")
    };

    public static List<Candidate> Candidates { get; set; } = new List<Candidate>
    {
        new Candidate("firstName", "lastName", "candidate@gmail.com", "phoneNumber", "specialization", DateTime.Now, CandidateIntent.Internship, AcademicDegree.Bachelor, "extraInfo"),
        new Candidate("firstName2", "lastName2", "candidate2@gmail.com", "phoneNumber", "specialization", DateTime.Now, CandidateIntent.Job, AcademicDegree.Doctorate, "extraInfo2")
    };

    public static Mock<IEventRepository> GetEventTemplateRepository()
    {
        var eventRepositoryMock = new Mock<IEventRepository>();
        eventRepositoryMock.Setup(repo => repo.FindById(It.IsAny<Guid>())).Returns(Task.FromResult(Events[0]));

        return eventRepositoryMock;
    }
}
