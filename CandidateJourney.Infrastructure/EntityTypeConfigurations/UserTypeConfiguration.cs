using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidateJourney.Infrastructure.EntityTypeConfigurations;

public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.EmailAddress).IsRequired();
        
        builder
            .HasIndex(u => u.EmailAddress)
            .IsUnique();

        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}