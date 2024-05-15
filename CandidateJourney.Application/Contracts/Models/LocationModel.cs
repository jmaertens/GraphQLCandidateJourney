namespace CandidateJourney.Application.Contracts.Models
{
    public class LocationModel
    {
        public LocationModel(Guid id, string name, string address, List<EventModel>? events = null)
        {
            Id = id;
            Name = name;
            Address = address;
            Events = events ?? new List<EventModel>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<EventModel> Events { get; set; }
    }
}