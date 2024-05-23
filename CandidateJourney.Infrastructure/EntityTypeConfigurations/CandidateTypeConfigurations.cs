using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidateJourney.Infrastructure.EntityTypeConfigurations
{
    public class CandidateTypeConfigurations : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(candidate => candidate.Id);
            builder.Property(candidate => candidate.Id).ValueGeneratedNever();
            builder.Property(candidate => candidate.FirstName).IsRequired();
            builder.Property(candidate => candidate.LastName).IsRequired();
            builder.Property(candidate => candidate.Email).IsRequired();
            builder.Property(candidate => candidate.PhoneNumber).IsRequired(false);
            builder.Property(candidate => candidate.Specialization).IsRequired(false);
            builder.Property(candidate => candidate.DateOfGraduation).IsRequired(false);
            builder.Property(candidate => candidate.CandidateType).IsRequired();
            builder.Property(candidate => candidate.GraduationType).IsRequired();
            builder.Property(candidate => candidate.PictureName).IsRequired(false);
            builder.Property(candidate => candidate.ExtraInfo).IsRequired(false);
        }
    }
}
