using Bogus;
using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Infrastructure
{
    public class DatabaseSeeder
    {
        private readonly CandidateJourneyDbContext _context;

        public DatabaseSeeder(CandidateJourneyDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (!_context.Users.Any())
            {
                var testUser = new User(Guid.Parse("149d6235-13e5-4149-bdbc-19f7ab8046a4"), "Test", "User", "test.user@example.com");
                var users = GenerateFakeUsers(10);
                _context.Users.Add(testUser);
                _context.Users.AddRange(users);
                _context.SaveChanges();
            }

            if (!_context.Locations.Any())
            {
                var locations = GenerateFakeLocations(50); 
                _context.Locations.AddRange(locations);
                _context.SaveChanges();
            }

            if (!_context.Events.Any())
            {
                var users = _context.Users.ToList();
                var locations = _context.Locations.ToList();
                var events = GenerateFakeEvents(5000, users, locations);
                _context.Events.AddRange(events);
                _context.SaveChanges();
            }
        }


        private List<Event> GenerateFakeEvents(int count, List<User> users, List<Location> locations)
        {
            var faker = new Faker<Event>()
                .CustomInstantiator(f =>
                {
                    var (startDateTime, endDateTime) = GenerateEventTimes(f);
                    
                    var eventItem = new Event(
                        $"{f.PickRandom(SeedData.TechWords)} {f.PickRandom(SeedData.EventDescriptors)} {startDateTime.Year}", // Name
                        f.Company.CompanyName(),          // Organizer
                        startDateTime,                    // StartDateTime
                        endDateTime,                      // EndDateTime
                        f.PickRandom<AudienceCategory>(), // TargetAudience
                        f.Lorem.Paragraph(2),             // Description
                        f.Internet.Url()                  // EventLink
                    )
                    {
                        Locations = locations.OrderBy(x => Guid.NewGuid()).Take(5).ToList(),
                        Candidates = new List<Candidate>()
                    };

                    var candidates = GenerateFakeCandidates(10);
                    eventItem.Candidates.AddRange(candidates);

                    return eventItem;
                })
                .RuleFor(e => e.CreatedBy, f => f.PickRandom(users))
                .RuleFor(e => e.CreatedOn, f => f.Date.Past())
                .RuleFor(e => e.IsDeleted, f => false);

            return faker.Generate(count);
        }

        private (DateTime StartDateTime, DateTime EndDateTime) GenerateEventTimes(Faker f)
        {
            DateTime startDate = f.Random.Bool(0.2f) ? f.Date.Past() : f.Date.Future();

            var startDateTime = new DateTime(
                startDate.Year, startDate.Month, startDate.Day,
                f.Random.Int(8, 17), f.PickRandom(new[] { 0, 30 }), 0);

            var endDateTime = startDateTime.AddHours(f.Random.Int(1, 8))
                                           .AddMinutes(f.PickRandom(new[] { 0, 30 }));

            if (endDateTime > startDateTime.AddDays(2))
            {
                endDateTime = startDateTime.AddDays(2).AddHours(-1 * (startDateTime.Hour - 17));
            }

            return (startDateTime, endDateTime);
        }

        private List<Candidate> GenerateFakeCandidates(int count)
        {
            var faker = new Faker<Candidate>()
                .CustomInstantiator(f => new Candidate(
                    f.Person.FirstName,
                    f.Person.LastName,
                    f.Person.Email,
                    f.Phone.PhoneNumber(),
                    f.Name.JobType(),
                    f.Date.Past(5), 
                    f.PickRandom<CandidateIntent>(),
                    f.PickRandom<AcademicDegree>(),
                    f.Lorem.Sentence()
                ));

            return faker.Generate(count);
        }

        private List<Location> GenerateFakeLocations(int count)
        {
            var faker = new Faker<Location>()
                .CustomInstantiator(f => new Location
                {
                    Id = Guid.NewGuid(),
                    Name = $"Location {f.IndexFaker + 1}",
                    Address = f.Address.StreetAddress()
                });

            return faker.Generate(count);
        }


        private List<User> GenerateFakeUsers(int count)
        {
            var faker = new Faker<User>()
                .CustomInstantiator(f => new User(
                    f.Person.FirstName,
                    f.Person.LastName,
                    f.Person.Email
                ));

            return faker.Generate(count);
        }
    }
}
