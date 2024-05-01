using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Infrastructure.EntityTypeConfigurations
{
    public class InterestTypeConfigurations : IEntityTypeConfiguration<Interest>
    {
        public void Configure(EntityTypeBuilder<Interest> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Name).IsRequired();
            builder.HasData(
                new Interest("Data Science") { Id = 21 },
                new Interest("Data Visualization") { Id = 1 },
                new Interest("Data Engineering / Data Governance") { Id = 2 },
                new Interest("Business Intelligence") { Id = 3 },
                new Interest("Microsoft Development") { Id = 4 },
                new Interest("Java Development") { Id = 5 },
                new Interest("Front-end Development") { Id = 6 },
                new Interest("Low code Development") { Id = 7 },
                new Interest("Business Automation") { Id = 8 },
                new Interest("SAP") { Id = 9 },
                new Interest("Infrastructure") { Id = 10 },
                new Interest("Digital Workplace") { Id = 11 },
                new Interest("CSV") { Id = 12 },
                new Interest("Quality & Compliance (life sciences)") { Id = 13 },
                new Interest("Security") { Id = 14 },
                new Interest("Requirements & Testing") { Id = 15 },
                new Interest("Business Consulting") { Id = 16 },
                new Interest("Financial Services") { Id = 17 },
                new Interest("Supply Chain Optimization") { Id = 18 },
                new Interest("Agile / Scrum") { Id = 19 },
                new Interest("UI / UX Design") { Id = 20 }

            );
        }

    }
}
