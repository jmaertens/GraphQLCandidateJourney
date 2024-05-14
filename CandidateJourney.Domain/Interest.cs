using System.Drawing;

namespace CandidateJourney.Domain
{
    public class Interest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Interest()
        {
            
        }

        public Interest(string name)
        {
            Name = name;
        }
    }
}
