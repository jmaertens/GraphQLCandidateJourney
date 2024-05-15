namespace CandidateJourney.Application.Contracts.Models
{
    public class LocationModel
    {
        public LocationModel()
        {
            
        }

        public LocationModel(Guid id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}