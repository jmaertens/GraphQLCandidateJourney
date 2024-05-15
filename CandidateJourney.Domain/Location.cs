namespace CandidateJourney.Domain
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Event> Events { get; set; }

        public Location()
        {
            Events = new List<Event>();
        }
    }
}
