using Bogus;
using CandidateJourney.Domain;
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
            
            if (!_context.Events.Any())
            {
                var users = _context.Users.ToList();
                var events = GenerateFakeEvents(1000, users);
                _context.Events.AddRange(events);
                _context.SaveChanges();
            }
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

        private List<Event> GenerateFakeEvents(int count, List<User> users)
        {
            var faker = new Faker<Event>()
                .CustomInstantiator(f =>
                {
                    var (startDateTime, endDateTime) = GenerateEventTimes(f);

                    return new Event(
                        $"{f.PickRandom(SeedData.TechWords)} {f.PickRandom(SeedData.EventDescriptors)} {startDateTime.Year}", // Name
                        f.Company.CompanyName(),          // Organizer
                        //f.PickRandom(SeedData.BelgianCities), // Location
                        startDateTime,                    // StartDateTime
                        endDateTime,                      // EndDateTime
                        f.PickRandom<AudienceCategory>(), // TargetAudience
                        f.Lorem.Paragraph(2),             // Description
                        f.Internet.Url()                  // EventLink
                    );
                })
                .RuleFor(e => e.CreatedBy, f => f.PickRandom(users))
                .RuleFor(e => e.CreatedOn, f => f.Date.Past())
                .RuleFor(e => e.Candidates, f => new List<Candidate>())
                .RuleFor(e => e.IsDeleted, f => false);
        
            return faker.Generate(count);
        }

        private (DateTime StartDateTime, DateTime EndDateTime) GenerateEventTimes(Faker f)
        {
            var startDate = f.Date.Future();
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
    }
}
