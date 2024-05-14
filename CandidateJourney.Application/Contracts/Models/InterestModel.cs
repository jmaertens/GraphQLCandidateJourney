namespace CandidateJourney.Application.Contracts.Models
{
    public class InterestModel
    {
        public InterestModel(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
