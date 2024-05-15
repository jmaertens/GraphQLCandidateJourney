﻿using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidateJourney.Infrastructure.Configurations
{
    public class LocationTypeConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.Address)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.HasMany(l => l.Events)
                   .WithMany(e => e.Locations)
                   .UsingEntity<Dictionary<string, object>>(
                        "EventLocation",
                        j => j.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                        j => j.HasOne<Location>().WithMany().HasForeignKey("LocationId")
                   );
        }
    }
}
