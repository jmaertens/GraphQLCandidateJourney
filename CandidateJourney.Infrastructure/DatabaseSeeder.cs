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
            if (!_context.Events.Any())
            {
                var events = GenerateFakeEvents(1000);
                _context.Events.AddRange(events);
                _context.SaveChanges();
            }
        }

        private List<Event> GenerateFakeEvents(int count)
        {
            var faker = new Faker<Event>()
                .CustomInstantiator(f =>
                {
                    var belgianCities = new[]
                    {
                        "Antwerp", "Bruges", "Brussels", "Ghent", "Leuven",
                        "Liège", "Mechelen", "Mons", "Namur", "Ostend"
                    };

                    var techWords = new[]
                    {
                        "AI", "Machine Learning", "Blockchain", "Cybersecurity", "Cloud Computing",
                        "DevOps", "Big Data", "IoT", "AR/VR", "Quantum Computing", "Data Science",
                        "Software Engineering", "Network Security", "Digital Transformation",
                        "Robotics", "5G Technology", "Edge Computing", "Natural Language Processing",
                        "Augmented Reality", "Virtual Reality", "FinTech", "HealthTech", "Bioinformatics",
                        "Quantum Cryptography", "Smart Cities", "Green IT", "Autonomous Vehicles",
                        "Microservices", "API Management", "Serverless Computing", "Wearable Technology",
                        "Digital Twins", "Smart Home", "Connected Devices", "Blockchain for Supply Chain",
                        "AI Ethics", "AI Governance", "AI for Social Good", "Tech for Good"
                    };

                    var eventDescriptors = new[]
                    {
                        "Conference", "Summit", "Workshop", "Seminar", "Expo",
                        "Meetup", "Symposium", "Webinar", "Forum", "Hackathon",
                        "Bootcamp", "Roundtable", "Panel Discussion", "Lecture", "Presentation",
                        "Training", "Networking Event", "Showcase", "Convention", "Tech Talk",
                        "Masterclass", "Q&A Session", "Launch Event", "Demo Day", "Product Reveal",
                        "Pitch Event", "Innovation Day", "Idea Lab", "Tech Fair", "Tech Symposium",
                        "Tech Expo", "Incubator Event", "Accelerator Event", "Startup Competition",
                        "Investor Meeting", "Tech Retreat", "Innovation Summit", "Tech Challenge",
                        "Code Fest", "App Jam"
                    };

                    var (startDateTime, endDateTime) = GenerateEventTimes(f);

                    return new Event(
                        $"{f.PickRandom(techWords)} {f.PickRandom(eventDescriptors)} {startDateTime.Year}", // Name
                        f.Company.CompanyName(),          // Organizer
                        f.PickRandom(belgianCities),      // Location
                        startDateTime,                    // StartDateTime
                        endDateTime,                      // EndDateTime
                        f.PickRandom<AudienceCategory>(), // TargetAudience
                        f.Lorem.Paragraph(2),             // Description
                        f.Internet.Url()                  // EventLink
                    );
                })
                .RuleFor(e => e.CreatedBy, f => new User(f.Person.FirstName, f.Person.LastName, f.Person.Email))
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
