namespace CandidateJourney.Domain
{
    public class Interest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Interest(string name)
        {
            Name = name;
        }
    }
}
