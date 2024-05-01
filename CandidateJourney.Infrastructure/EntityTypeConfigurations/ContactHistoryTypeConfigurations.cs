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
    public class ContactHistoryTypeConfigurations: IEntityTypeConfiguration<ContactHistory>
    {
        public void Configure(EntityTypeBuilder<ContactHistory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedOn).IsRequired();

            builder.HasOne(x => x.CreatedBy).WithMany().OnDelete(DeleteBehavior.NoAction).IsRequired().HasForeignKey("CreatedById");
        }
    }
}
