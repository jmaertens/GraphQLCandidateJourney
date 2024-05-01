﻿using CandidateJourney.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CandidateJourney.Infrastructure.EntityTypeConfigurations
{
    public class EventTypeConfigurations : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Organizer).IsRequired();
            builder.Property(x => x.Location).IsRequired();
            builder.Property(x => x.EventLink).IsRequired(false);
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.StartDateTime).IsRequired();
            builder.Property(x => x.EndDateTime).IsRequired(false);
            builder.Property(x => x.TargetAudience).IsRequired();
            builder.Property(x => x.UpdatedOn).IsRequired(false);

            builder.HasMany(x => x.Candidates).WithOne().IsRequired();
            builder.HasOne(x => x.CreatedBy).WithMany().HasForeignKey("CreatedById");
            builder.HasOne(x => x.UpdatedBy).WithMany().HasForeignKey("UpdatedById").IsRequired(false);

            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
